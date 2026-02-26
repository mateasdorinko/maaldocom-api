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
COPY ["src/MaaldoCom.Api/MaaldoCom.Api.csproj", "src/MaaldoCom.Api/"]
COPY ["src/MaaldoCom.Api.Infrastructure/MaaldoCom.Api.Infrastructure.csproj", "src/MaaldoCom.Api.Infrastructure/"]
COPY ["src/MaaldoCom.Api.Application/MaaldoCom.Api.Application.csproj", "src/MaaldoCom.Api.Application/"]
COPY ["src/MaaldoCom.Api.Domain/MaaldoCom.Api.Domain.csproj", "src/MaaldoCom.Api.Domain/"]
RUN dotnet restore "src/MaaldoCom.Api/MaaldoCom.Api.csproj"
COPY . .
WORKDIR "/src/src/MaaldoCom.Api"
RUN dotnet build "./MaaldoCom.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MaaldoCom.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK --interval=30s --timeout=5s --retries=3 \
  CMD wget -qO- http://localhost:8080/health || exit 1
ENTRYPOINT ["dotnet", "MaaldoCom.Api.dll"]
