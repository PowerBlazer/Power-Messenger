using Microsoft.AspNetCore.Mvc;
using PowerMessenger.Infrastructure.Persistence.Context;

namespace PowerMessenger.WebApi.Controllers;

public class TestController:ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test([FromServices] EfContext context)
    {
        return Ok(context.Messages?.ToList());
    }
}