using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PowerMessenger.Infrastructure.Identity;
using PowerMessenger.Infrastructure.Persistence;
using PowerMessenger.Application;
using PowerMessenger.Application.Layers.Identity;
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

builder.Services.AddCors(coreOptions =>
    coreOptions.AddPolicy("All", options =>
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    }));

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

#region AuthenticationConfiguration

var jwtOptions = builder.Configuration.GetSection("JWT")
    .Get<JwtOptions>()!;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        //options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });


#endregion

var app = builder.Build();

app.UseSwaggerSetup(app.Services
    .GetRequiredService<IApiVersionDescriptionProvider>());

app.UseStaticFiles();
app.UseCors("All");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
app.MigrateDatabase();

app.Run();