using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.WebApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]"),ApiVersion("1.0"),Authorize]
public class MessageController: BaseController
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получение сообщении с первого не прочитанного сообщения группового чата 
    /// </summary>
    /// <param name="chatId">Id чата</param>
    /// <returns></returns>
    /// <response code="200">Возвращает список сообщении группового чата</response>
    /// <response code="401">Пользователь не авторизвован</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpGet("groupChat/{chatId:long}")]
    [ProducesResponseType(typeof(ApiActionResult<IList<MessageGroupChatResponse>>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ApiActionResult<IList<MessageGroupChatResponse>>> GetMessagesGroupChatByUser(
        [FromRoute] long chatId)
    {
        var result = await _mediator.Send(new GetMessagesGroupChatQuery(chatId,UserId));

        return new ApiActionResult<IList<MessageGroupChatResponse>>
        {
            Result = result
        };
    }
}