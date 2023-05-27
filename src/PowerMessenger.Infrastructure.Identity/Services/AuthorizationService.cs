using System.Security.Claims;
using System.Text.Json;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Layers.Identity.Services;
using PowerMessenger.Application.Layers.MessageQueues.UserRegistered;
using PowerMessenger.Application.Layers.MessageQueues.VerificationEmailSend;
using PowerMessenger.Application.Layers.Redis.Services;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.Exceptions;
using PowerMessenger.Infrastructure.Identity.Common;
using PowerMessenger.Infrastructure.Identity.Entities;
using PowerMessenger.Infrastructure.Identity.Helpers;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity.Services;

public class AuthorizationService: IAuthorizationService
{
    private readonly IRedisService _redisService;
    private readonly IVerificationEmailSendProducer _verificationEmailSendProducer;
    private readonly IUserRegisteredProducer _userRegisteredProducer;
    private readonly ITokenService _tokenService;
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IIdentityTokenRepository _tokenRepository;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    
    public AuthorizationService(IRedisService redisService, 
        IVerificationEmailSendProducer verificationEmailSendProducer, 
        ITokenService tokenService, 
        IIdentityUserRepository identityUserRepository,
        IUserRegisteredProducer userRegisteredProducer, 
        IIdentityUnitOfWork identityUnitOfWork, 
        IIdentityTokenRepository tokenRepository)
    {
        _redisService = redisService;
        _verificationEmailSendProducer = verificationEmailSendProducer;
        _tokenService = tokenService;
        _identityUserRepository = identityUserRepository;
        _userRegisteredProducer = userRegisteredProducer;
        _identityUnitOfWork = identityUnitOfWork;
        _tokenRepository = tokenRepository;
    }
    
    public async Task<string> SendEmailVerificationCodeAsync(string email)
    {
         var verificationCode = VerificationCode.GenerateVerificationCode();
         var sessionId = Guid.NewGuid().ToString();
         
         var verifyMailSession = new SessionVerifyEmail
         {
             Email = email,
             VerificationCode = verificationCode,
             IsOk = false
         };
         
         var isAdded = await _redisService
             .SetValueAsync(sessionId,verifyMailSession.ToString(),TimeSpan.FromMinutes(5));

         if (!isAdded)
         {
             throw new Exception($"Сессия не создана {sessionId}");
         }

         await _verificationEmailSendProducer.PublishEmailSend(new VerificationEmailSendEvent(
             email,
             confirmLink:"",
             verificationCode));

         return sessionId;
    }

    public async Task<string> ResendVerificationCodeAsync(string sessionId,string email)
    {
        var sessionJson = await _redisService.GetValueAsync(sessionId);

        if (sessionJson is null)
        {
            throw new SessionNotFoundException("Сессия не найдена");
        }
        
        var verificationCode = VerificationCode.GenerateVerificationCode();

        var newSession = new SessionVerifyEmail
        {
            Email = email,
            VerificationCode = verificationCode,
            IsOk = false
        };

        var isUpdate = await _redisService.UpdateValueAsync(
            sessionId, 
            newSession.ToString(), 
            TimeSpan.FromMinutes(5));

        if (!isUpdate)
        {
            throw new Exception($"Сессия не обновлена {sessionId}");
        }
        
        await _verificationEmailSendProducer.PublishEmailSend(new VerificationEmailSendEvent(email,
            confirmLink:"",
            verificationCode));

        return sessionId;
    }

    public async Task VerifyEmailCodeAsync(string sessionId,string verifyCode)
    {
        var sessionValue = await _redisService.GetValueAsync(sessionId);

        if (sessionValue is null)
        {
            throw new SessionNotFoundException("Сессия закончилась");
        }

        var sessionVerifyEmail = JsonSerializer.Deserialize<SessionVerifyEmail>(sessionValue);

        if (verifyCode != sessionVerifyEmail!.VerificationCode)
        {
            throw new SessionCodeNotValidException("Неправильный код верификации");
        }
        
        sessionVerifyEmail.IsOk = true;

        var isUpdate = await _redisService.UpdateValueAsync(
            sessionId,
            sessionVerifyEmail.ToString(),
            TimeSpan.FromMinutes(5));

        if (!isUpdate)
        {
            throw new Exception($"Сессия не обновлена {sessionId}");
        }
    }

    public async Task<RegistrationResult> RegisterUserAsync(RegistrationInput registrationInput)
    {
        var sessionJson = await _redisService.GetValueAsync(registrationInput.SessionId);

        if (sessionJson is null)
        {
            throw new SessionNotFoundException("Сессия подтверждения закончилась, повторите попытку");
        }

        var session = JsonSerializer.Deserialize<SessionVerifyEmail>(sessionJson);

        if (!session!.IsOk)
        {
            throw new SessionCodeNotValidException("Сессия не подтверждена");
        }

        var newIdentityUser = new IdentityUser
        {
            Email = session.Email,
            EmailConfirmed = true,
            DateCreated = DateTime.Now,
            PasswordHash = ComputeHash256.ComputeSha256Hash(registrationInput.Password)
        };

        string accessToken = null!, refreshToken = null!;

        await _identityUnitOfWork.ExecuteWithExecutionStrategyAsync(async () =>
        {
            var identityUser = await _identityUserRepository.AddUserAsync(newIdentityUser);
        
            await _userRegisteredProducer.PublishUserRegistered(new UserRegisteredEvent(
                identityUser.Id,
                registrationInput.UserName
            ));
        
             accessToken = _tokenService.GenerateAccessToken(identityUser);
             refreshToken = await _tokenService.GenerateRefreshTokenAsync(identityUser.Id);

            await _redisService.DeleteValueAsync(registrationInput.SessionId);
        });

        return new RegistrationResult(accessToken, refreshToken);
    }

    public async Task<LoginResult> LoginUserAsync(LoginInput loginInput)
    {
        var identityUser = await _identityUserRepository.GetUserByEmailAsync(loginInput.Email);

        if (identityUser is null)
        {
            throw new AuthenticationValidException("Email","Пользователь с такой почтой не зарегестрирован");
        }

        if (identityUser.PasswordHash != ComputeHash256.ComputeSha256Hash(loginInput.Password))
        {
            throw new AuthenticationValidException("Password","Неправильный пароль");
        }
        
        string accessToken = null!, refreshToken = null!;

        await _identityUnitOfWork.ExecuteWithExecutionStrategyAsync(async () =>
        {
            accessToken = _tokenService.GenerateAccessToken(identityUser);
            refreshToken = await _tokenService.UpdateRefreshTokenAsync(identityUser.Id);
        });
        
        return new LoginResult(accessToken, refreshToken);
    }

    public async Task<RefreshTokenResult> RefreshToken(RefreshTokenInput refreshTokenInput)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenInput.AccessToken);

        var userId = long.Parse(principal.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value);
        var identityToken = await _tokenRepository.GetTokenByUserId(userId);

        if (identityToken.Token != refreshTokenInput.RefreshToken
            || identityToken.Expiration <= DateTime.Now)
        {
            throw new AuthenticationValidException("Token", "Недействительный токен обновления");
        }
        
        string accessToken = null!, refreshToken = null!;

        await _identityUnitOfWork.ExecuteWithExecutionStrategyAsync(async () =>
        {
            var identityUser = await _identityUserRepository.GetUserByIdAsync(userId);
            
            accessToken = _tokenService.GenerateAccessToken(identityUser!);
            refreshToken = await _tokenService.UpdateRefreshTokenAsync(identityUser!.Id);
        });

        return new RefreshTokenResult(accessToken, refreshToken);
    }
}