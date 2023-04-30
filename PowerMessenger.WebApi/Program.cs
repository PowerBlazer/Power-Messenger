using System.Text.Json.Serialization;
using FluentValidation;
using PowerMessenger.Infrastructure.Identity;
using PowerMessenger.Infrastructure.Persistence;
using PowerMessenger.Application;
using PowerMessenger.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

#region MediatrService

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

AssemblyScanner.FindValidatorsInAssembly(typeof(Program).Assembly)
    .ForEach(item => builder.Services.AddScoped(item.InterfaceType, item.ValidatorType));

#endregion

#region BusinessService
builder.Services
    .AddInfrastructurePersistence(builder.Configuration)
    .AddInfrastructureIdentity(builder.Configuration)
    .AddApplication(builder.Configuration);
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MigrateDatabase();

app.Run();