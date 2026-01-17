# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MusicLearningLibrary.Api/MusicLearningLibrary.Api.csproj MusicLearningLibrary.Api/
COPY MusicLearningLibrary.Application/MusicLearningLibrary.Application.csproj MusicLearningLibrary.Application/
COPY MusicLearningLibrary.Domain/MusicLearningLibrary.Domain.csproj MusicLearningLibrary.Domain/
COPY MusicLearningLibrary.Infrastructure/MusicLearningLibrary.Infrastructure.csproj MusicLearningLibrary.Infrastructure/

RUN dotnet restore MusicLearningLibrary.Api/MusicLearningLibrary.Api.csproj

# Copy all files
COPY . .

# Build and publish of the API
WORKDIR /src/MusicLearningLibrary.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000

COPY --from=build /app/publish .

EXPOSE 5000

# Set entrypoint
ENTRYPOINT ["dotnet", "MusicLearningLibrary.Api.dll"]