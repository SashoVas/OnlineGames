FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "OnlineGames.Web/OnlineGames.Web.csproj"

RUN dotnet build "OnlineGames.Web/OnlineGames.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineGames.Web/OnlineGames.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineGames.Web.dll"]
