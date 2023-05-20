using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PowerMessenger.Infrastructure.Identity;
using PowerMessenger.Infrastructure.Persistence;
using PowerMessenger.Application;
using PowerMessenger.Infrastructure.Email;
using PowerMessenger.Infrastructure.MessageQueues;
using PowerMessenger.Infrastructure.Redis;
using PowerMessenger.WebApi;
using PowerMessenger.WebApi.Common;

var builder = WebApplication.CreateBuilder(args);

#region BaseConfiguration

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor();

builder.Services.AddSwaggerConfiguration();

#endregion

#region BusinessServices

builder.Services
    .AddInfrastructurePersistence(builder.Configuration)
    .AddInfrastructureIdentity(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddMessageQueue(builder.Configuration)
    .AddRedis(builder.Configuration)
    .AddEmail(builder.Configuration);
#endregion

var app = builder.Build();

app.UseSwaggerSetup(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
app.MigrateDatabase();

app.Run();