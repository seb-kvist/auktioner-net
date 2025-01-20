# auktioner-net
Using MVC and CoreIdentity to create an auction house-based website where you can register and log in as two different roles.

This is a .NET 9.0 web application using ASP.NET Core and Entity Framework Core with an in-memory database for development purposes.
Requirements

Before running the project, ensure you have the following installed on your system:

    .NET 9.0 SDK or later
    A code editor, such as Visual Studio (recommended) or Visual Studio Code

Packages

The project uses the following NuGet packages. These are automatically restored when you build the project:

    Microsoft.AspNetCore.Identity.EntityFrameworkCore (9.0.0)
    Microsoft.AspNetCore.Identity.UI (9.0.0)
    Microsoft.EntityFrameworkCore (9.0.0)
    Microsoft.EntityFrameworkCore.InMemory (9.0.0)
    Microsoft.EntityFrameworkCore.Tools (9.0.0)
    Microsoft.NET.Test.Sdk (17.12.0)
    Microsoft.VisualStudio.Web.CodeGeneration.Design (9.0.0)
    xunit (2.9.3)
    xUnit.runner.visualstudio (3.0.1)

You do not need to install these manually. They will be restored by running dotnet restore.
Setup Instructions

    Clone this repository:

git clone https://github.com/seb-kvist/auktioner-net.git
cd auktioner-net

Restore the required packages:

dotnet restore

Run the application:

    dotnet run

    Open your browser and navigate to https://localhost:5001 (or the port specified in the console output).

Testing

To run tests, use the following command:

dotnet test

Features

    ASP.NET Core Identity for user authentication
    Entity Framework Core with an in-memory database for development
    Unit testing using xUnit
