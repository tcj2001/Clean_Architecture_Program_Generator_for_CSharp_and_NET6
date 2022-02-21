# Clean Architecture Solution Program Generator  
Program Generator to **generate** Clean Architecture base project structure solution containing Repositories, Services and Controllers.  

---
## Table of Contents
- [Getting Started](#getting-started)
- Installed Files
    * [Clean Architecture Database Access Generator.tt](#clean-architecture-database-access-generator.tt)
    * [Clean Architecture WebAPI Generator.tt](#clean-architecture-webapi-generator.tt)
    * [T4Helper.ttinclude](#t4Helper.ttinclude) 
- [Github Link](#github-link)
- [Installing template from Nuget Package Manager](#installing-template-from-nuget-package-manager)
- [How to check if this really works](#how-to-check-if-this-really-works)
    * Adding new entities
      * [Code First approach](#code-first-approach)
      * [Database First approach](#database-first-approach)
- [Some useful commands](#some-useful-commands)

---
# Getting Started
This is a NuGet package should be installed as a template in dotnet cli.  
You can browse this package using manage NuGet packages by searching for "Thomson Mathews" or "Clean_Architecture_Program_Generator_for_CSharp_and_NET6"  
![Imgur](https://i.imgur.com/9N0loaV.png)  
This will install two template files in your project.  
[Clean Architecture Database Access Generator.tt](#clean-architecture-database-access-generator.tt)  
[Clean Architecture WebAPI Generator.tt](#clean-architecture-webapi-generator.tt)  
![Imgur](https://i.imgur.com/8R9B6rS.png)  
Once these template files are installed.   

Run the all templates using "Transform All T4 Templates" from the build menu.  
![Imgur](https://i.imgur.com/G6tc5vK.png)  
or
Run individual template by right click on the template and select "Run Custom Tool"
![Imgur](https://i.imgur.com/ZIdjSuG.png)  

This will generate the following projects.  
![Imgur](https://i.imgur.com/mbJYKGZ.png)  
Now define entities in the Domain Project using code first approach or database first approach 
![Imgur](https://i.imgur.com/eW5s1uW.png)  
and run the transformation again.  

**Voila! all repositories, services and controller are generated for you by the templates.**  

Now just set WebAPI as a startup project and run it, that's it.  

---
## Clean Architecture Database Access Generator.tt
This template will generate 4 projects:  
Domain, Application, Persistence, Startup.  
![Imgur](https://i.imgur.com/IqyphaF.png)  

**Domain Project:**  
Define all entities in Entities folder, either using code first approach or database approach, Exceptions folder defines some basic useful exceptions that can be used in global error handling, Interface folder contains GenreicRepository, RepositoryManager and UnitOfWork interfaces that is implemented in the Persistence project.  
![Imgur](https://i.imgur.com/i7ZTVsl.png)  

**Application Project**  
Defines Interface for ServiceManager in the Interfaces folder, will also generate repository interface for each entity defined in the Domain project. ServiceManger is implemented in the Services Folder, it will also contain Services for each entities defined in the Domain project.  
![Imgur](https://i.imgur.com/PZGqqWL.png)  

**Persistence Project:**     
ApplicationDbContext should be defined or generated using Entity Framework in the context folder, Repositories folder contains GenericRepository, RepositoryManager and UnitOfWork implementation.  
![Imgur](https://i.imgur.com/rG56cfd.png)  

**Startup Projects:**  
This is a console application that will create a Generic Host which will help in dependency injection, here ApplicationDbContext is added to the ServiceCollection, this project will help you to run Entity framework commands to do entity migrations to a database or generate entities and ApplicationDbContext from existing database.  
![Imgur](https://i.imgur.com/vZZcObV.png)  

Startup project is only used to run Entity framework commands with out a WebAPI project and is not required if WebAPI project is generated.  

*Once you add more entities, you can **run this template again to generate** repository interfaces for the new Entities in the Domain Project, repository implementation for the new Entities in Persistence Project and Services and its Service interfaces for the new Entities in the Application Project*.  

---
## Clean Architecture WebAPI Generator.tt
This template will generate open a dialog box, select ASP.NET Core WebAPI project and it will create a project named **WebAPI**
![Imgur](https://i.imgur.com/V7yfIId.png)  

**WebAPI Project:**  
This contains controllers and other required classes.  
![Imgur](https://i.imgur.com/f6AZMa9.png)  

**Controller Folder:**  
Contains generated controller for each entities defined in Domain project.  

**Extensions Folder:**  
Contains extension for adding ServiceManager dependency, RepositoryManger dependency, AddplicationDBcontext dependency and Extension method to add BasicAuthMiddleware to the HTTP request pipeline.    
Modify AddPersistenceExtension class in this folder to refer to correct connection string.  
![Imgur](https://i.imgur.com/rRvAvrW.png)

**MiddleWare Folder:**     
Contains implementation for BasicAuthMiddleware and ExceptionHandlingMiddleware.  

**Appsetting.json:**   
Contains configuration for Serilog logging, connection string and basic authentication details.  

**Appsetting Folder:**   
Contains classes structure to read from appsettings.json file.    

**Program.cs**
This connect all the wiring between the projects.
![Imgur](https://i.imgur.com/QBq9PMJ.png)  
![Imgur](https://i.imgur.com/v1JkFNe.png)  

*Once you add more entities, you can **run this template again to generate** controllers for the new Entities in the WebAPI Project.*  

---
## T4Helper.ttinclude
This file contains function used by the above T4Template files.

---
## Github Link
https://github.com/tcj2001/Clean_Architecture_Program_Generator_for_CSharp_and_NET6  

---
## Installing template from Nuget Package Manager
From your package manager console.  
**dotnet new -i Clean_Architecture_Program_Generator_for_CSharp_and_NET6_Solution**  
This will add a new template in your dotnet cli environment.  
![Imgur](https://i.imgur.com/7Z6Ixu9.png)  

**dotnet new -l**  
you can use the above command to check if the template was installed. 
![Imgur](https://i.imgur.com/wLcdyyv.png)  

**CA_PG_C#_NET6_SOL** is shortname for the template

There are **two way** to use the template.

**First Method**.  
Go to a directory where you want to make use of the template and type command.   
**dotnet new CA_PG_C#_NET6_SOL -o YourProjectFolderName** 
![Imgur](https://i.imgur.com/yqz8I1l.png)  

This will create a ClassLibrary project named Clean_Architecture_Program_Generator_for_CSharp_and_NET6 within the solution  
![Imgur](https://i.imgur.com/qDOcoGn.png)  

**Second Method**.  
When you select new project in visual studio you will see a template named Clean_Architecture_Program_Generator_for_CSharp_and_NET6, if you don't see filter the project type and select Clean Architecture, select this template and create your project.   
![Imgur](https://i.imgur.com/tlgWJjY.png)  

This will create a ClassLibrary project named Clean_Architecture_Program_Generator_for_CSharp_and_NET6 within YourProjectFolderName.    
![Imgur](https://i.imgur.com/XBmdPGL.png)  
  
---
## How to check if this really works
Once all the projects are generated using the T4Templates  
By default Domain project will contains one entity named Sample.cs, it also generated Repository and Services for sample entity.  
The classes highlighted in green will get generated for **each entity** defined in the Domain project entities folder.    
The Classes highlighted in red gets re-generated every time with entity details.  
![Imgur](https://i.imgur.com/9AAuzNp.png)  

## Code First approach 
Since we already have a sample entity defined for you by the template, I will just make use of it.   
Just for simplicity let's create a SQLite database to hold a table for Sample entity in app.db by running these commands.  
**dotnet ef migrations add "initialmigration2" --project Persistence --startup-project WebAPI**    
**dotnet ef database update --project Persistence --startup-project WebAPI**  
![Imgur](https://i.imgur.com/UDldadJ.png)  

This will create App.db in WebAPI folder with a table named Sample.  

In the appsettings.json we already have two connections string pre-defined.  
![Imgur](https://i.imgur.com/7mh67uJ.png)  

Make sure class AddPersistenceExtension in WebAPI project Extensions Folder has  
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Name=SqliteDb"));
![Imgur](https://i.imgur.com/eab4hcV.png)  

**Set WebAPI as a startup project and run it.**  
![Imgur](https://i.imgur.com/OGxNSBs.png)  


## Database First approach
Here we will make use of a BookStores database in my Sql Server setup. 
This database was grabbed from internet, credit goes to the person who made it available for community.
![Imgur](https://i.imgur.com/7mh67uJ.png)  

Make sure class AddPersistenceExtension in WebAPI project Extensions Folder has   
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Name=SqlServerDb"))
![Imgur](https://i.imgur.com/iMsZMGg.png)  

Run this command with a connection string pointing to your Sql Server Database.  
**scaffold-DbContext -Connection "Name=SqlServerDb" -Provider Microsoft.EntityFrameworkCore.SqlServer -Project Persistence -StartupProject WebAPI -OutputDir ..\Domain\Entities -Context ApplicationDbContext -ContextDir ..\Persistence\Context -Namespace Domain.Entities -ContextNamespace Persistence.Context -DataAnnotations -force**  

This will generate all entities in domain project  
![Imgur](https://i.imgur.com/RN1FCHt.png)  

Run all templates 
![Imgur](https://i.imgur.com/G6tc5vK.png)  

or individual template
![Imgur](https://i.imgur.com/ZIdjSuG.png)  

All repositories, services and controllers are generated for you.  
![Imgur](https://i.imgur.com/drJXm4q.png)  
![Imgur](https://i.imgur.com/m8tcvzR.png)  

Set WebAPI as a startup project and run it.  
![Imgur](https://i.imgur.com/bBLUjZD.png)  

By default the WebAPI uses BasicAuthentication the userid and password is defined in the appsettings.json file  
![Imgur](https://i.imgur.com/SoB2Ame.png)  

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
