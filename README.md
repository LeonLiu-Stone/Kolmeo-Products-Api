# Kolmeo-Products-Api
A code test for Kolmeo

## Requirements
Create a simple service that can be used to add and retrieve products via a public endpoint.
Use this as an opportunity to flex your engineering and create a small production ready api.

### Product Model
```
{
  Id
  Name
  Description
  Price	
}
```

### Stack

- ASP.NET Core
- Entity Framework Core

### Not required
- Authentication
- Persistence

## About this code test.

NB: As this is a common api for products CRUD, so parts of codes are copied from my existing project.

### Dependency Injection
  Looger: default .net logging
  DBContext:  KolmeoDataContext
  ProductProvider: for CRUD

### EF
  SqlLite is used for saving products in DB
  KolmeoDataContext is the DB Context for products

  To make SqlLite works on your local 
  ```bash
  dotnet tool install --global dotnet-ef
	dotnet add package Microsoft.EntityFrameworkCore.Design
	dotnet ef migrations add InitialCreate
	dotnet ef database update
  ```

### API Features
  Swagger
  APIVersioning
  HealthCheck

### XUnit
  As the limit time, I just write test cases for GetAll and GetById endpoints in controller, to show my test skills,
  
  TODO:
    test cases for Create/Update/Delete in controller
    test cases for ProductProvider

## How to run
  Api.Products is the startup project
  Option 1: 
    Run in VS Studio with F5
  Option 2: run in console
    ```bash
    dotnet run
    ```
