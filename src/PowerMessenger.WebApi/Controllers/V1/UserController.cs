using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerMessenger.Application.Features.UserFeature.GetUserData;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.User;

namespace PowerMessenger.WebApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/user"),ApiVersion("1.0"),Authorize]
public class UserController: BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiActionResult<UserDataResponse>> GetUserData()
    {
        var result = await _mediator.Send(new GetUserDataQuery(UserId));

        return new ApiActionResult<UserDataResponse>
        {
            Result = result
        };
    }
}