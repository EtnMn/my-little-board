## My Little Board

# My Little Board

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
| AzureAd:Instance                         | https://login.microsoftonline.com/                                                          |
| AzureAd:TenantId                         | Azure tenant Id                                                                             |
| AzureAd:ClientId                         | Azure application client Id                                                                 |
| AzureAd:ClientSecret                     | Application client secret                                                                   |
| AzureAd:Domain                           | Azure application domain                                                                    |
| AzureAd:CallbackPath                     | /signin-oidc                                                                                |
| GraphApi:BaseUrl                         | https://graph.microsoft.com/beta                                                            |
| GraphApi:Scopes                          | Array of scopes                                                                             |
| Logging:LogLevel:Default                 | Information                                                                                 |
| Logging:LogLevel:Microsoft.AspNetCore    | Warning                                                                                     |
| AllowedHosts                             | *                                                                                           |

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
