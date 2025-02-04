# My Little Board

[![Build & test "My Little Board"](https://github.com/EtnMn/my-little-board/actions/workflows/test.yml/badge.svg)](https://github.com/EtnMn/my-little-board/actions/workflows/test.yml)

This is a solution for managing projects.

## Description

The My Little Board application allows users to create and manage projects. Each project can contain multiple properties, which can be used to organize, notes, or any other information.

## Features

- Create new project
- Collaborate with other users

## Technologies Used

- .NET 8
- C#
- Blazor Server
- Entity Framework Core
- HTML/CSS
- MudBlazor

## Getting Started

To get started with the My Little Board application, follow these steps:

1. Clone the repository
2. Open the solution in Visual Studio
3. Build the solution
4. Run the application

### Server configuration

| Key                                      | Value                                                                                       |
|------------------------------------------|---------------------------------------------------------------------------------------------|
| Azure:Entra:Instance                     | https://login.microsoftonline.com/                                                          |
| Azure:Entra:TenantId                     | Azure tenant Id                                                                             |
| Azure:Entra:ClientId                     | Azure application client Id                                                                 |
| Azure:Entra:ClientSecret                 | Application client secret                                                                   |
| Azure:Entra:CallbackPath                 | /signin-oidc                                                                                |
| GraphApi:BaseUrl                         | https://graph.microsoft.com/beta                                                            |
| GraphApi:Scopes                          | Array of scopes                                                                             |
| Logging:LogLevel:Default                 | Information                                                                                 |
| Logging:LogLevel:Microsoft.AspNetCore    | Warning                                                                                     |
| AllowedHosts                             | *                                                                                           |
| ConnectionStrings:Default                | Connection string to database                                                               | 
| ASPNETCORE_ENVIRONMENT                   | Development                                                                                 |

Connection string example:
Web app
Server=tcp:<server>.database.windows.net,1433;Initial Catalog=<database>;Authentication=Active Directory Managed Identity
GitHub (e.g.: deploy action & github secrets)
Server=tcp:<server>.database.windows.net,1433;Initial Catalog=<database>;Authentication=Active Directory Default
Local
Server=(localdb)\\MSSQLLocalDB;Database=xxx;Trusted_Connection=True;


### Application roles

Add an App role named _Administrator_ in application registration in Entra Id.
Then assign role to administrator user in Entra Id Enterprise Application.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
