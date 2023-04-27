FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PowerMessenger.IdentityServer/PowerMessenger.IdentityServer.csproj", "PowerMessenger.IdentityServer/"]
RUN dotnet restore "PowerMessenger.IdentityServer/PowerMessenger.IdentityServer.csproj"
COPY . .
WORKDIR "/src/PowerMessenger.IdentityServer"
RUN dotnet build "PowerMessenger.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PowerMessenger.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PowerMessenger.IdentityServer.dll"]
