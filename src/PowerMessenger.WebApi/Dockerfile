﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PowerMessenger.WebApi/PowerMessenger.WebApi.csproj", "PowerMessenger.WebApi/"]
COPY ["PowerMessenger.Infrastructure.Shared/PowerMessenger.Infrastructure.Shared.csproj", "PowerMessenger.Infrastructure.Shared/"]
COPY ["PowerMessenger.Infrastructure.Persistence/PowerMessenger.Infrastructure.Persistence.csproj", "PowerMessenger.Infrastructure.Persistence/"]
COPY ["PowerMessenger.Application/PowerMessenger.Application.csproj", "PowerMessenger.Application/"]
COPY ["PowerMessenger.Domain/PowerMessenger.Domain.csproj", "PowerMessenger.Domain/"]
COPY ["PowerMessenger.Infrastructure.Identity/PowerMessenger.Infrastructure.Identity.csproj", "PowerMessenger.Infrastructure.Identity/"]
COPY ["PowerMessenger.Infrastructure.MessageQueues/PowerMessenger.Infrastructure.MessageQueues.csproj", "PowerMessenger.Infrastructure.MessageQueues/"]
COPY ["PowerMessenger.Infrastructure.Redis/PowerMessenger.Infrastructure.Redis.csproj", "PowerMessenger.Infrastructure.Redis/"]
COPY ["PowerMessenger.Infrastructure.Email/PowerMessenger.Infrastructure.Email.csproj", "PowerMessenger.Infrastructure.Email/"]

RUN dotnet restore "PowerMessenger.WebApi/PowerMessenger.WebApi.csproj"
COPY . .
WORKDIR "/src/PowerMessenger.WebApi"
RUN dotnet build "PowerMessenger.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PowerMessenger.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PowerMessenger.WebApi.dll"]
