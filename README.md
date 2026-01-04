# Maaldocom.Services

## Setup

### Docker Compose

#### SQL Server

_docker-compose.yml_

```yaml
services:
  sqlserver2022:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver2022
    environment:
      ACCEPT_EULA: Y
      MSSQL_SA_PASSWORD: ${MSSQL_SA_PASSWORD}
      MSSQL_PID: Developer # Developer edition
    ports:
      - "1433:1433"
    volumes:
      - data:/var/opt/mssql
    restart: unless-stopped

volumes:
  data:
```

_.env_

```yaml
MSSQL_SA_PASSWORD="MY_SUPER_SECRET_PASSWORD"
```

#### Azurite

_docker-compose.yml_

```yaml
```

_.env_

```yaml
```

### Local User Secrets

[MaaldoCom.Services.Api Readme](/src/MaaldoCom.Services.Api/README.md)

### Migrations

[MaaldoCom.Services.Infrastructure Readme](/src/MaaldoCom.Services.Infrastructure/README.md)