using PowerMessenger.Application.Hubs;

namespace PowerMessenger.WebApi.Common;

public static class SignalHubsConfiguration
{
    public static void UseSignalRHubs(this WebApplication app)
    {
        app.MapHub<ChatHub>("/chat");
        app.MapHub<MessageHub>("/message");
    }
}