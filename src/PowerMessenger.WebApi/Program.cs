using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using PowerMessenger.Infrastructure.Identity;
using PowerMessenger.Infrastructure.Persistence;
using PowerMessenger.Application;
using PowerMessenger.Application.Layers.Identity;
using PowerMessenger.Infrastructure.Email;
using PowerMessenger.Infrastructure.MessageQueues;
using PowerMessenger.Infrastructure.Redis;
using PowerMessenger.Infrastructure.Shared;
using PowerMessenger.WebApi;
using PowerMessenger.WebApi.Common;
using PowerMessenger.WebApi.Middleware;

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
    .AddHttpContextAccessor()
    .AddSignalR();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddCors(coreOptions =>
    coreOptions.AddPolicy("All", options =>
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.WithOrigins("http://127.0.0.1:5500/");
        options.AllowCredentials();
        options.SetIsOriginAllowed(_ => true);
    }));

#endregion

#region BusinessServices

builder.Services
    .AddInfrastructurePersistence(builder.Configuration)
    .AddInfrastructureIdentity(builder.Configuration)
    .AddInfrastructureShared(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddMessageQueue(builder.Configuration)
    .AddRedis(builder.Configuration)
    .AddEmail(builder.Configuration);

builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();
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
            ClockSkew = TimeSpan.FromSeconds(30)
        };
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/chat"))
                {
                    context.Token = accessToken;
                }
                
                return Task.CompletedTask;
            }
        };
        
    });


#endregion

var app = builder.Build();

app.UseSwaggerSetup();
app.UseStaticFiles();
app.UseCors("All");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSignalRHubs();
app.MapControllers();

app.MigrateDatabase();

app.Run();