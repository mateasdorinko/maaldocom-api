# `MaaldoCom.Services.Cli`

<img src="assets/logo.svg" width="100" />

## CLI Setup

These commands are intended to be run from the `MaaldoCom.Services.Cli` project directory.

### OpenAPI Document Specification Generation

The Cli contains a method to generate a client api proxy defined by the OpenAPI document specifications in 
the API project. The process is baked into the build process of the Cli project, so simply building the project will
generate the client proxy. The definition for building via Refit can be found in the `maaldo-api.refitter` file in the
project.


