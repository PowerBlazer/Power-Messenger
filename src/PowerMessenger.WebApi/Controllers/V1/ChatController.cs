using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerMessenger.Application.Features.ChatFeature.GetChatsByUser;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.Chat;

namespace PowerMessenger.WebApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/chat"),ApiVersion("1.0"),Authorize]
public class ChatController: BaseController
{
    private readonly IMediator _mediator;
    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получение чатов пользователя
    /// </summary>
    /// <remarks></remarks>
    /// <returns>Возвращает список чатов</returns>
    /// <response code="200">Возвращает список чатов</response>
    /// <response code="401">Пользователь не авторизвован</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpGet("chats")]
    [ProducesResponseType(typeof(ApiActionResult<IList<ChatResponse>>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ApiActionResult<IList<ChatResponse>>> GetChatsByUser()
    {
        var result = await _mediator.Send(new GetChatsByUserQuery(UserId));

        return new ApiActionResult<IList<ChatResponse>>
        {
            Result = result
        };
    }
}