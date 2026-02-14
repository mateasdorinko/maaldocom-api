FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["src/MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj", "src/MaaldoCom.Services.Api/"]
COPY ["src/MaaldoCom.Services.Infrastructure/MaaldoCom.Services.Infrastructure.csproj", "src/MaaldoCom.Services.Infrastructure/"]
COPY ["src/MaaldoCom.Services.Application/MaaldoCom.Services.Application.csproj", "src/MaaldoCom.Services.Application/"]
COPY ["src/MaaldoCom.Services.Domain/MaaldoCom.Services.Domain.csproj", "src/MaaldoCom.Services.Domain/"]
RUN dotnet restore "src/MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj"
COPY . .
WORKDIR "/src/src/MaaldoCom.Services.Api"
RUN dotnet build "./MaaldoCom.Services.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MaaldoCom.Services.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK --interval=30s --timeout=5s --retries=3 \
  CMD wget -qO- http://localhost:8080/health || exit 1
ENTRYPOINT ["dotnet", "MaaldoCom.Services.Api.dll"]
