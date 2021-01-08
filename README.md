# EOrchestral Briefcase

An app for storing and managing orchestral briefcases and orchestral piece sheets. The app consist of simple Blazor WebAssembly project as SPA (Single Page Application) UI and ASP .NET Core REST API which UI is calling to. The project was created by following the principles of Clean Architecture.

The app was created mainly to satisfy personal needs as i wanted to have all orchestral piece sheets in one place, easily accesible from any device in more organized way.

## Technologies

* .NET Core 3.1 (C# 8)
* ASP .NET Core 3.1
* Entity Framework Core 3.1
* Blazor WebAssembly (.NET 5)
* Bootstrap 4
* AutoMapper 10
* xUnit 2

## Getting Started

1. Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download)
2. Get the repository with `git clone https://github.com/sebastianstudniczek/EOrchestralBriefcase` or just download a zip.
3. Navigate to `Source/WebAPI` and run `dotnet run` to launch the back end (ASP .NET Core Web API)
4. Navigate to `Source/BlazorUI` and run `dotnet run` to launch the front end (Blazor WebAssembly)

## Database Configuration

For ensuring that all users will be able to run the solution without needing to set up an additional infrastructure (e.g. SQL Server), the project is configured to use an in-memory database by default.

To change this, you will need to update **WebAPI/appsettings.json** as follows:

```json
    "UseInMemoryDatabase": false,
```
Verify that the **Default Connection** connection string within **appsettings.json** points to a valid SQL Server instance.



