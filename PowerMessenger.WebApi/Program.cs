using System.Text.Json.Serialization;
using FluentValidation;
using PowerMessenger.Infrastructure.Identity;
using PowerMessenger.Infrastructure.Persistence;
using PowerMessenger.Application;
using PowerMessenger.Infrastructure.Email;
using PowerMessenger.Infrastructure.MessageQueues;
using PowerMessenger.Infrastructure.Redis;
using PowerMessenger.WebApi;

var builder = WebApplication.CreateBuilder(args);

#region BaseConfiguration

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHttpContextAccessor();

#endregion

#region MediatrService

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

AssemblyScanner.FindValidatorsInAssembly(typeof(Program).Assembly)
    .ForEach(item => builder.Services.AddScoped(item.InterfaceType, item.ValidatorType));

#endregion

#region BusinessService

builder.Services
    .AddInfrastructurePersistence(builder.Configuration)
    .AddInfrastructureIdentity(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddMessageQueue(builder.Configuration)
    .AddRedis(builder.Configuration)
    .AddEmail(builder.Configuration);
#endregion

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();    
//}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MigrateDatabase();

app.Run();