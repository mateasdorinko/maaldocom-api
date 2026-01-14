# Maaldocom.Services

## Projects

### Src

- [MaaldoCom.Services.Api](/src/MaaldoCom.Services.Api/README.md)
- [MaaldoCom.Services.Application](/src/MaaldoCom.Services.Application/README.md)
- [MaaldoCom.Services.Cli](/src/MaaldoCom.Services.Cli/README.md)
- [MaaldoCom.Services.Domain](/src/MaaldoCom.Services.Domain/README.md)
- [MaaldoCom.Services.Infrastructure](/src/MaaldoCom.Services.Infrastructure/README.md)

### Tests

- [Tests.Integration](/tests/Tests.Integration/README.md)
- [Tests.Unit.Api](/tests/Tests.Unit.Api/README.md)
- [Tests.Unit.Application](/tests/Tests.Unit.Application/README.md)
- [Tests.Unit.Cli](/tests/Tests.Unit.Cli/README.md)
- [Tests.Unit.Domain](/tests/Tests.Unit.Domain/README.md)
- [Tests.Unit.Infrastructure](/tests/Tests.Unit.Infrastructure/README.md)

## Solution Setup

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
services:
  azurite-emulator:
    image: mcr.microsoft.com/azure-storage/azurite
    container_name: azurite-emulator
    restart: unless-stopped
    command: "azurite --blobHost 0.0.0.0 --queueHost 0.0.0.0 --tableHost 0.0.0.0 --location /data --debug /data/debug.log"
    ports:
      - 10000:10000 # blob storage service
      - 10001:10001 # queue storage service
      - 10002:10002 # table storage service
    volumes:
      - data:/data

volumes:
  data:
```

_.env_

```yaml
```

### Other Setup

- [Local User Secrets](/src/MaaldoCom.Services.Api/README.md#local-user-secrets)
- [Entity Framework](/src/MaaldoCom.Services.Infrastructure/README.md#entity-framework)



