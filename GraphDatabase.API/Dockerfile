FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GraphDatabase.API/GraphDatabase.API.csproj", "GraphDatabase.API/"]
COPY ["GraphDatabase.Infrastructure/GraphDatabase.Infrastructure.csproj", "GraphDatabase.Infrastructure/"]
COPY ["GraphDatabase.Entities/GraphDatabase.Entities.csproj", "GraphDatabase.Entities/"]
RUN dotnet restore "GraphDatabase.API/GraphDatabase.API.csproj"
COPY . .
WORKDIR "/src/GraphDatabase.API"
RUN dotnet build "GraphDatabase.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GraphDatabase.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphDatabase.API.dll"]
