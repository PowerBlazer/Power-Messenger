using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerMessenger.Application.Features.MessageFeature.SendMessage;
using PowerMessenger.Application.Features.MessageFeature.SetMessageAsRead;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.WebApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/message"),ApiVersion("1.0"),Authorize]
public class MessageController: BaseController
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Отметить сообщение последним прочитанным в чате
    /// </summary>
    /// <param name="chatId">Id чата</param>
    /// <param name="messageId">Id сообщения</param>
    /// <returns></returns>
    /// <response code="200">Сообщение отмечено прочитанным</response>
    /// <response code="401">Пользователь не авторизвован</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPut("{messageId:long}/read")]
    [ProducesResponseType(typeof(ApiActionResult<SetMessageAsReadResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiActionResult<SetMessageAsReadResponse>),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ApiActionResult<SetMessageAsReadResponse>> MarkMessageAsRead(
        [FromRoute] long messageId, long chatId)
    {
        var result = await _mediator.Send(new SetMessageAsReadCommand(chatId, messageId, UserId));

        return new ApiActionResult<SetMessageAsReadResponse>
        {
            Result = result
        };
    }
    
    /// <summary>
    /// Отправить сообщение в чат
    /// </summary>
    /// <param name="sendMessageCommand"></param>
    /// <returns></returns>
    /// <response code="200">Сообщение отправлено</response>
    /// <response code="401">Пользователь не авторизвован</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPost("send")]
    [ProducesResponseType(typeof(ApiActionResult<MessageResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiActionResult<MessageResponse>),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ApiActionResult<MessageResponse>> SendMessage([FromBody]SendMessageCommand sendMessageCommand)
    {
        sendMessageCommand.UserId = UserId;
        
        var result = await _mediator.Send(sendMessageCommand);

        return new ApiActionResult<MessageResponse>
        {
            Result = result
        };
    }
    
}