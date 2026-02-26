<img src="/assets/logo.svg" width="100" />

# MaaldoCom.Api.Infrastructure

## Infrastructure Setup

These commands are intended to be run from the `MaaldoCom.Api.Infrastructure` project directory.

### Entity Framework

#### Migration Requisites

Before creating or applying migrations, ensure that you have the following prerequisites installed:

```shell
dotnet tool install --global dotnet-ef
```

#### Migration Commands

##### Adding a Migration

Migration commands must be executed in the `MaaldoCom.Api.Infrastructure` project, while the startup project is
set to `MaaldoCom.Api`. This ensures that the correct configuration and dependencies are used.

```shell
dotnet ef migrations add [MIGRATION_NAME] --output-dir Database/Migrations --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```

##### Applying Migrations

```shell
dotnet ef database update --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```

##### Removing a Migration

```shell
dotnet ef migrations remove --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```
### FFMpeg

FFMpeg is used for media album picture and videos processing tasks. Ensure that
FFMpeg must be installed on your system and accessible via the system PATH call from the
MaaldoCom.Api.Infrastructure project.

#### Installation

##### Debian

```shell
sudo apt update
sudo apt install ffmpeg
```

##### Windows

1. Download the latest static build from the [FFMpeg website](https://ffmpeg.org/download.html).
2. Extract the downloaded archive to a folder of your choice.
3. Add the `bin` folder of the extracted files to your system PATH:
   - Right-click on 'This PC' or 'Computer' on the desktop or in File Explorer.
   - Select 'Properties'.
   - Click on 'Advanced system settings'.
   - Click on the 'Environment Variables' button.
   - In the 'System variables' section, find the 'Path' variable and select it.
   - Click 'Edit', then 'New', and add the path to the `bin` folder.
   - Click 'OK' to close all dialog boxes.
4. Open a new Command Prompt window and type `ffmpeg -version` to verify the installation.


