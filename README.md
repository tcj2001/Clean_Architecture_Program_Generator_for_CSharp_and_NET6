# Clean Architecture Solution Program Generator
This will install templates that allows to generate clean architecture project structure within your solution containing Domain, Application, Persistence, WebAPI projects  

Domain project will contain by default an sample entity and intefaces for repositories, unitOfWork  
Application project will contain interfaces for the services and its implementaion along with Service Manager
Persistence project will contain ApplicationDbContext, implemetaion for repositories and UnitOfWork, along with Repository Manager
WebAPI project will contain Controllers for the the entities  

**When you add additional Entities in the Domain Project, running this templates will automatically genarate all required Repositories, Services and Controllers** 

---
## Table of Contents
- [Clean Architecture Solution Program Generator](#clean-architecture-solution-program-generator)
- [Github Link](#github-link)
- [Installing using Nuget Package Manager](#installing-using-nuget-package-manager)
- [Installing as a Template](#installing-as-a-template)
- Installed Files
    * [Clean Architecture Database Access Generator.tt](#clean-architecture-database-access-generator.tt)
    * [Clean Architecture WebAPI Generator.tt](#clean-architecture-webapi-generator.tt)
    * [T4Helper.ttinclude](#t4Helper.ttinclude) 
- [How to check if this really works](#how-to-check-if-this-really-works)
- [Some useful commands](#some-useful-commands)

---
## Github Link
 
https://github.com/tcj2001/Clean_Architecture_Program_Generator_for_CSharp_and_NET6  

---
## Installing using Nuget Package Manager
**Installation Procedure**  
Create a ClassLibrary or a Console Project, make sure the project name is different from the solution name or uncheck "Place solution and project in the same diectory"  

Install Nuget Package  
**Clean_Architecture_Program_Generator_for_CSharp_and_NET6_Solution**  

Once you install the Nuget package you will see three required files added to your project  
**Clean Architecture Database Access Generator.tt**  
**Clean Architecture WebAPI Generator.tt**  
**T4Helper.ttinclude**  
Rest of them you can ignore

*Change the Custom Tool property to **TextTemplatingFileGenerator** on the above .tt files to generate your projects as detailed in the following section*  

---
## Installing as a Template
Another way to install this as a **template** is to use package manager console  
**dotnet new -i Clean_Architecture_Program_Generator_for_CSharp_and_NET6_Solution**  
This will add a new template in your donet cli environment  

**dotnet new -l**  
will show a entry like this, to confrim the template was installed 
**Clean_Architecture_Program_Generator_for_CSharp_and_NET6_Solution  CA_PG_C#_NET6_SOL    [C#]        Web/ASP.NET/Clean Architecture**         
**CA_PG_C#_NET6_SOL** is shortname for the template  

There are **two way** to use the template

First Method  
Go to a directory where you want to make use of the template  
**dotnet new CA_PG_C#_NET6_SOL -o YourProjectFolderName**  
This will create a ClassLibrary project named Clean_Architecture_Program_Generator_for_CSharp_and_NET6 within the solution

Second Method  
When you select new project in visual studio you will see a template named Clean_Architecture_Program_Generator_for_CSharp_and_NET6, if you don't see filter the project type and select Clean Architecture, select this template and create your project   
This will create a ClassLibrary project named Clean_Architecture_Program_Generator_for_CSharp_and_NET6 within YourProjectFolderName    

---
## Clean Architecture Database Access Generator.tt
This template will generate 4 projects:  
Domain, Application, Persistence, Startup  

Application Project refers Domain project  
Persistence Project refers Applications Project  
Startup or WebApi Project refers Application Project for business logic and Persistence project to activate Dbcontext  

**Domain Project:**  
Define all entities in Entities folder, either using code first approach or database approach, Exceptions folder defines some basic usefull exceptions that can be used in global error handling, Interface folder contains GenreicRepository, RepositoryManager and UnitOfWork interfaces that is impletmented in the Persistence project  
**Application Project**  
Defines Interface for ServiceManager in the Interfaces folder, will also generate repository interface for each entity defined in the Domain project. ServiceManger is implemented in the Services Folder, it will also contain Services for each entities defined in the Domain project  
**Persistence Project:**     
ApplicationDbContext should be defined or generated using Entity Framework in the context folder, Repositories folder contains GenericRepository, RepositoryManager and UnitOfWork implementation  
**Startup Projects:**  
This is a console application that will create a Generic Host which will help in dependency injection, here ApplicationDbContext is added to the ServiceCollection, this project will help you to run Entity framework commands to do entity migrations to a database or generate entities and ApplicationDbContext from existing database  

Startup project is only used to run Entity framework commands with out a WebAPI project and is not required if WebAPI project is generated  


*Once you add more entities, you can **run this template again to generate** repository interfaces for the new Entities in the Domain Project, repository implementation for the new Entities in Persistence Project and Services and its Service interfaces for the new Entities in the Application Project*  

---
## Clean Architecture WebAPI Generator.tt
This template will generate open a dialog box, select ASP.NET Core WebAPI project and it will create a project named **WebAPI**
**WebAPI Project:**  

Controller Folder:  
Contains generated controller for each entities defined in Domain project

Extensions Folder:  
Contains extension for adding ServiceManager dependency, RepositoryManger dependency, AddplicationDBcontext dependency and Extension method to add BasicAuthMiddleware to the HTTP request pipeline  
Modify AddPersistenceExtension class in this folder to refer to correct connection string

MiddleWare Folder:   
Contains implementation for BasicAuthMiddleware and ExceptionHandlingMiddleware

Appsetting.json:   
Contains configuration for Serilog logging, connection string and basic authentication details

Appsetting Folder:   
Contains classes structure to read from appsettings.json file

*Once you add more entities, you can **run this template again to generate** controllers for the new Entities in the WebAPI Project*  

---
## How to check if this really works
Once all the projects are generated using the T4Templates  
By default Domain project will contains one entity named Sample.cs, it also generated Repository and Services for sample entity  

Set WebAPI as you startup project

*For code first approach*  
Let create a SQLite database to hold a table for Sample entity in app.db by running these commands  
**dotnet ef migrations add "initialmigration2" --project Persistence --startup-project WebAPI**    
**dotnet ef database update --project Persistence --startup-project WebAPI**  
This will create App.db in WebAPI folder with a table named Sample  
Make sure class AddPersistenceExtension in WebAPI project Extensions Folder has  
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Name=SqliteDb"));

*For database first approach*  
Run this command with a connection string pointing to your Sql Server Database  
**scaffold-DbContext -Connection "Server=DESKTOP-GBANT4V; Database=BookStoresDB; Trusted_Connection=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -Project Persistence -StartupProject Startup -OutputDir ..\Domain\Entities -Context ApplicationDbContext -ContextDir ..\Persistence\Context -Namespace Domain.Entities -ContextNamespace Persistence.Context -DataAnnotations -Force**  
This will generate all entities in domain project  
Run Custom Tool on Clean Architecture Database Access Generator.tt file again to generate all Repositories and services for the new entities  
Run Custom Tool on Clean Architecture WebAPI Generator.tt file again to generate all Controllers in the WebAPI project  
Modify AddPersistenceExtension class in WebAPI projects Extensions folder to refer to correct connection string
Make sure class AddPersistenceExtension in WebAPI project Extensions Folder has   
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Name=SqlServerDb"))

Set WebAPI as a startup project and run it 
By default the WebAPI uses BasicAuthentication the userid and password is defined in the appsettings.json file  

---
## T4Helper.ttinclude
This file contains function used by the above T4Template files

---
## Some useful commands

**Code First Migrations**  
dotnet ef migrations add "initialmigration1" --project Persistence --startup-project Startup  
dotnet ef migrations add "initialmigration2" --project Persistence --startup-project WebAPI

dotnet ef database update --project Persistence  --startup-project Startup  
dotnet ef database update --project Persistence  --startup-project WebAPI  

**Database First Migrations**  
scaffold-DbContext -Connection "Server=DESKTOP-GBANT4V; Database=BookStoresDB; Trusted_Connection=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -Project Persistence -StartupProject Startup -OutputDir ..\Domain\Entities  -Context ApplicationDbContext -ContextDir ..\Persistence\Context -Namespace Domain.Entities -ContextNamespace Persistence.Context  -DataAnnotations -Force

If possible do not use connection string directly in the above command.  

if you have an appsettings.json define connection strings as shown  
  "ConnectionStrings": {
    "SqliteDB": "DataSource=app.db;Cache=Shared",
    "SqlServerDB": "Server=DESKTOP-GBANT4V; Database=BookStoresDB; Trusted_Connection=True;"
  }  
then use "Name=SqliteDB" in the scaffold-DbContext -Connection parameter


**Remove migration**  
dotnet ef migrations remove

**List migrations**    
dotnet ef migrations list

**Install entity frame work**    
dotnet tool install --global dotnet-ef  
dotnet tool update --global dotnet-ef  

**Create a solution with the current director name**    
dotnet new solution

**Create a startup project if webapi or mvc not used**    
dotnet new worker -n Startup

**Create class library**    
dotnet new classlib -o projectName

**Add solution file**    
dotnet sln add SomeFolder/SomeProject.csproj AnotherFolder/AnotherProject.csproj 

**Add project dependency**    
dotnet add SomeFolder/SomeProject.csproj reference AnotherFolder/AnotherProject.csproj

**Add package dependencies**    
dotnet add SomeFolder/SomeProject.csproj package microsoft.entityframeworkcore
