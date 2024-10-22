# Use the official .NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy and restore all projects
# COPY ["OnlineGames.Web/OnlineGames.Web.csproj", "Web/"]
# COPY ["OnlineGames.Data/OnlineGames.Data.csproj", "Data/"]
# COPY ["OnlineGames.Services/OnlineGames.Services.csproj", "Services/"]
# COPY ["OnlineGames.Logic/OnlineGames.Logic.csproj", "Logic/"]

# Restore the Web project and its dependencies
# Copy all the source code
COPY . .
RUN dotnet restore "OnlineGames.Web/OnlineGames.Web.csproj"

# Build the Web project (builds all dependencies too)
RUN dotnet build "OnlineGames.Web/OnlineGames.Web.csproj" -c Release -o /app/build

# Publish the Web project
FROM build AS publish
RUN dotnet publish "OnlineGames.Web/OnlineGames.Web.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineGames.Web.dll"]
