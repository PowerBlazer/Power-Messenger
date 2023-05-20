using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;
using PowerMessenger.Application.Features.AuthorizationFeature.ResendConfirmationCode;
using PowerMessenger.Application.Features.AuthorizationFeature.SendEmailVerificationCode;
using PowerMessenger.Application.Features.AuthorizationFeature.VerifyEmailCode;
using PowerMessenger.Domain.Common;

namespace PowerMessenger.WebApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController: BaseController
{
    private readonly IMediator _mediator;
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Отправка сообщения на почту с подтверждением
    /// </summary>
    /// <param name="verificationCommand">Почта для отправки сообщения</param>
    /// <remarks></remarks>
    /// <returns>Возвращает id сессии</returns>
    /// <response code="200">Возвращает id сессии(string)</response>
    /// <response code="400">Неправильный формат почты или почта уже зарегестрирована</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPost("SendEmailVerification")]
    [ProducesResponseType(typeof(TActionResult<string>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TActionResult<string>),StatusCodes.Status400BadRequest)]
    public async Task<TActionResult<string>> SendEmailVerification(
        [FromBody]SendEmailVerificationCommand verificationCommand)
    {
        var result = await _mediator.Send(verificationCommand);
        
        return new TActionResult<string>
        {
            Result = result
        };
    }
    
    /// <summary>
    /// Подтверждение почты с помощью кода верификации 
    /// </summary>
    /// <param name="verifyEmailCodeCommand">Код верификации и Id Сессии</param>
    /// <returns>Возвращает результат подтверждения сессии(bool)</returns>
    /// <response code="200">Возвращает результат подтверждения сессии(bool)</response>
    /// <response code="400">Неправильный формат данных</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPut("ConfirmEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ConfirmEmailWithVerificationCode(
        [FromBody] VerifyEmailCodeCommand verifyEmailCodeCommand)
    {
        await _mediator.Send(verifyEmailCodeCommand);
    }
    
    /// <summary>
    /// Повторная отправка сообщения на почту с подтверждением
    /// </summary>
    /// <param name="resendConfirmationCodeCommand">Почта и сессия для отправки сообщения</param>
    /// <returns>Возвращает id сессии</returns>
    /// <response code="200">Сообщение отправлено</response>
    /// <response code="400">Неправильный формат почты</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPut("ResendEmailVerification")]
    [ProducesResponseType(typeof(TActionResult<string>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TActionResult<string>),StatusCodes.Status400BadRequest)]
    public async Task<TActionResult<string>> ResendEmailVerification(
        [FromBody] ResendConfirmationCodeCommand resendConfirmationCodeCommand)
    {
        var result = await _mediator.Send(resendConfirmationCodeCommand);
        
        return new TActionResult<string>
        {
            Result = result
        };
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="registerUserCommand">Пароль и имя пользователя</param>
    /// <returns>Возвращает AccessToken и RefreshToken</returns>
    /// <response code="200">Возвращает AccessToken и RefreshToken</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPost("Registration")]
    [ProducesResponseType(typeof(TActionResult<RegistrationResult>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TActionResult<RegistrationResult>),StatusCodes.Status400BadRequest)]
    public async Task<TActionResult<RegistrationResult>> RegisterUser(
        [FromBody] RegisterUserCommand registerUserCommand)
    {
        var result = await _mediator.Send(registerUserCommand);

        return new TActionResult<RegistrationResult>
        {
            Result = result
        };
    }



}