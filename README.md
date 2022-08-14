# Yummyr Data API
## What is it?
A data API linked to an SQL database to retrieve and insert Recipe and Ingredient models, using ASP.NET and EntityFramework Core

## What does it do?
Controllers receive http requests and perform CRUD actions on recipes and related models

## How do I get set up?
### You will need:
- Visual Studio 2022 or above
- .NET 6 SDK
- Something to test the API is working such as Postman

(I will eventually have some front end for this, but for now requests are the only way to interact with it)

### How to run:
Build the solution in Visual Studio. Using the package manager console, please enter the command `Update-Database` (this applies any migrations in the migrations folder)  
  
Press `F5` to run in debug or `ctrl + F5` to run without debug locally. You can fire requests with postman