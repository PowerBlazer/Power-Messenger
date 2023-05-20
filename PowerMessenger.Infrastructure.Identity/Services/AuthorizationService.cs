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
    private readonly JwtOptions _jwtOptions;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    
   
    public AuthorizationService(IRedisService redisService, 
        IVerificationEmailSendProducer verificationEmailSendProducer, 
        ITokenService tokenService, 
        IIdentityUserRepository identityUserRepository,
        JwtOptions jwtOptions, 
        IUserRegisteredProducer userRegisteredProducer, 
        IIdentityUnitOfWork identityUnitOfWork)
    {
        _redisService = redisService;
        _verificationEmailSendProducer = verificationEmailSendProducer;
        _tokenService = tokenService;
        _identityUserRepository = identityUserRepository;
        _jwtOptions = jwtOptions;
        _userRegisteredProducer = userRegisteredProducer;
        _identityUnitOfWork = identityUnitOfWork;
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
        
             accessToken = _tokenService.GenerateAccessToken(identityUser, _jwtOptions);
             refreshToken = await _tokenService.GenerateRefreshTokenAsync(identityUser.Id, _jwtOptions);

            await _redisService.DeleteValueAsync(registrationInput.SessionId);
        });

        return new RegistrationResult(accessToken, refreshToken);
    }
}