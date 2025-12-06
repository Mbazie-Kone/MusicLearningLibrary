# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MusicLibrary.Api/MusicLibrary.Api.csproj MusicLibrary.Api/
COPY MusicLibrary.Application/MusicLibrary.Application.csproj MusicLibrary.Application/
COPY MusicLibrary.Domain/MusicLibrary.Domain.csproj MusicLibrary.Domain/
COPY MusicLibrary.Infrastructure/MusicLibrary.Infrastructure.csproj MusicLibrary.Infrastructure/

RUN dotnet restore MusicLibrary.Api/MusicLibrary.Api.csproj

# Copy all files
COPY . .

# Build and publish of the API
WORKDIR /src/MusicLibrary.Api
RUN dotnet publish -c Release -o /app/publish

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy output published from the build stage
COPY --from=build /app/publish .

# Set entrypoint
ENTRYPOINT ["dotnet", "MusicLibrary.Api.dll"]