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
This is a Nuget package should be installed as a template in dotnet cli
![Nuget Package](https://github.com/tcj2001/Clean_Architecture_Program_Generator_for_CSharp_and_NET6/blob/master/_images/NugetPackage.png)  

This will install two template files in your project.  
[Clean Architecture Database Access Generator.tt](#clean-architecture-database-access-generator.tt)  
[Clean Architecture WebAPI Generator.tt](#clean-architecture-webapi-generator.tt)  
![Template Project](_images/TemplateProject.png)

Once these template files are installed.   

Run the all templates using "Transform All T4 Templates" from the build menu.  
![Run Templates](_images/RunTemplates.png)  

or
Run individual template by right click on the template and select "Run Custom Tool"
![Run Templates2](_images/RunTemplates2.png)

This will generate the following projects.  
![Generated Projects](_images/GeneratedProjects.png)  

Now define entities in the Domain Project using code first approach or database first approach 
![Entities Folder](_images/EntitiesFolder.png)  
and run the transformation again.  

**Voila! all repositories, services and controller are generated for you by the templates.**  

Now just set WebAPI as a startup project and run it, that's it.  

---
## Clean Architecture Database Access Generator.tt
This template will generate 4 projects:  
Domain, Application, Persistence, Startup.  
![Generated Projects1](_images/GeneratedProjects1.png)  
**Domain Project:**  
Define all entities in Entities folder, either using code first approach or database approach, Exceptions folder defines some basic usefull exceptions that can be used in global error handling, Interface folder contains GenreicRepository, RepositoryManager and UnitOfWork interfaces that is impletmented in the Persistence project.  
![Domain Project](_images/DomainProject.png)  

**Application Project**  
Defines Interface for ServiceManager in the Interfaces folder, will also generate repository interface for each entity defined in the Domain project. ServiceManger is implemented in the Services Folder, it will also contain Services for each entities defined in the Domain project.  
![Application Project](_images/ApplicationProject.png)  

**Persistence Project:**     
ApplicationDbContext should be defined or generated using Entity Framework in the context folder, Repositories folder contains GenericRepository, RepositoryManager and UnitOfWork implementation.  
![Persistence Project](_images/PersistenceProject.png)  

**Startup Projects:**  
This is a console application that will create a Generic Host which will help in dependency injection, here ApplicationDbContext is added to the ServiceCollection, this project will help you to run Entity framework commands to do entity migrations to a database or generate entities and ApplicationDbContext from existing database.  
![Startup Project](_images/StartupProject.png)  
Startup project is only used to run Entity framework commands with out a WebAPI project and is not required if WebAPI project is generated.  

*Once you add more entities, you can **run this template again to generate** repository interfaces for the new Entities in the Domain Project, repository implementation for the new Entities in Persistence Project and Services and its Service interfaces for the new Entities in the Application Project*.  

---
## Clean Architecture WebAPI Generator.tt
This template will generate open a dialog box, select ASP.NET Core WebAPI project and it will create a project named **WebAPI**
![Dialog Box](_images/DialogBox.png)  

**WebAPI Project:**  
![Generated Projects2](_images/GeneratedProjects2.png)  

**Controller Folder:**  
Contains generated controller for each entities defined in Domain project.  

**Extensions Folder:**  
Contains extension for adding ServiceManager dependency, RepositoryManger dependency, AddplicationDBcontext dependency and Extension method to add BasicAuthMiddleware to the HTTP request pipeline.    
Modify AddPersistenceExtension class in this folder to refer to correct connection string.  
![Add Persistence Extension1](_images/AddPersistenceExtension1.png)  

**MiddleWare Folder:**     
Contains implementation for BasicAuthMiddleware and ExceptionHandlingMiddleware.  

**Appsetting.json:**   
Contains configuration for Serilog logging, connection string and basic authentication details.  

**Appsetting Folder:**   
Contains classes structure to read from appsettings.json file.    

![Web A P I Project](_images/WebAPIProject.png)  

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
This will add a new template in your donet cli environment.  
![Template](_images/Template.png)  

**dotnet new -l**  
you can use the above command to check if the template was installed. 
![Check Template](_images/CheckTemplate.png)  

**CA_PG_C#_NET6_SOL** is shortname for the template

There are **two way** to use the template.

**First Method**.  
Go to a directory where you want to make use of the template and type command.   
**dotnet new CA_PG_C#_NET6_SOL -o YourProjectFolderName** 
![First Method1](_images/FirstMethod1.png)  

This will create a ClassLibrary project named Clean_Architecture_Program_Generator_for_CSharp_and_NET6 within the solution  
![First Method2](_images/FirstMethod2.png)  

**Second Method**.  
When you select new project in visual studio you will see a template named Clean_Architecture_Program_Generator_for_CSharp_and_NET6, if you don't see filter the project type and select Clean Architecture, select this template and create your project.   
![Second Method1](_images/SecondMethod1.png)  

This will create a ClassLibrary project named Clean_Architecture_Program_Generator_for_CSharp_and_NET6 within YourProjectFolderName.    
![Second Method2](_images/SecondMethod2.png)  

---
## How to check if this really works
Once all the projects are generated using the T4Templates  
By default Domain project will contains one entity named Sample.cs, it also generated Repository and Services for sample entity.  
The classes highlited in green will get generated for **each entity** defined in the Domain project entities folder
The Classes highlighted in red gets re-generated every time with entity details
![Generated Classes](_images/GeneratedClasses.png)  

## Code First approach 
Since we already have a sample entity defined for you by the template, I will just make use of it.   
Just for simplicity let's create a SQLite database to hold a table for Sample entity in app.db by running these commands.  
**dotnet ef migrations add "initialmigration2" --project Persistence --startup-project WebAPI**    
**dotnet ef database update --project Persistence --startup-project WebAPI**  
![Code First](_images/CodeFirst.png)  
This will create App.db in WebAPI folder with a table named Sample.  

In the appsettings.json we already have two connections string pre-defined.  
![Appsettings Connectionstring](_images/appsettingsConnectionstring.png)

Make sure class AddPersistenceExtension in WebAPI project Extensions Folder has  
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Name=SqliteDb"));
![Add Persistence Extension2](_images/AddPersistenceExtension2.png)  

**Set WebAPI as a startup project and run it.**  
![Code First Output](_images/CodeFirstOutput.png)

## Database First approach
Here we will make use of a BookStores database in my Sql Server setup.  
![Appsettings Connectionstring](_images/appsettingsConnectionstring.png)

Make sure class AddPersistenceExtension in WebAPI project Extensions Folder has   
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Name=SqlServerDb"))
![Add Persistence Extension3](_images/AddPersistenceExtension3.png)  

Run this command with a connection string pointing to your Sql Server Database.  
**scaffold-DbContext -Connection "Name=SqlServerDb" -Provider Microsoft.EntityFrameworkCore.SqlServer -Project Persistence -StartupProject WebAPI -OutputDir ..\Domain\Entities -Context ApplicationDbContext -ContextDir ..\Persistence\Context -Namespace Domain.Entities -ContextNamespace Persistence.Context -DataAnnotations -force**  
![Databse First1](_images/DatabseFirst1.png)

This will generate all entities in domain project  
![Databse First2](_images/DatabseFirst2.png)

Run all templates 
![Run Templates](_images/RunTemplates.png)  
or individual template
![Run Templates2](_images/RunTemplates2.png)

All repositories, services and controllers are generated for you.  
![Databse First3](_images/DatabseFirst3.png)
![Databse First4](_images/DatabseFirst4.png)

Set WebAPI as a startup project and run it.  
![Database First Output](_images/DatabaseFirstOutput.png)   

By default the WebAPI uses BasicAuthentication the userid and password is defined in the appsettings.json file  
![Basic Authentication Credentials](_images/BasicAuthenticationCredentials.png)

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
