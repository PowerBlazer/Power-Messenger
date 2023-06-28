
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace PowerMessenger.Application.Hubs;

public class BaseHub: Hub
{
    internal long UserId => long.Parse(Context.User.Claims
        .FirstOrDefault(p=>p.Type == ClaimTypes.NameIdentifier)?.Value!);
}