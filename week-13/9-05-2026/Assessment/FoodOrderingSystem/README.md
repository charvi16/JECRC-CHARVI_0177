# FoodHub - Online Food Ordering System

ASP.NET Core MVC food ordering system with Identity authentication, SQL Server, Entity Framework Core, admin management, customer cart, checkout, order history, invoices, Web API, Docker, and Azure DevOps pipeline.

## Default Admin

- Email: `admin@foodhub.local`
- Password: `Admin@12345`

## Run Locally

Update `appsettings.json` if your SQL Server differs, then run:

```powershell
dotnet restore
dotnet run
```

The database and seed data are created automatically on first run.

## Run With Docker

```powershell
docker compose up --build
```

Open `http://localhost:8080`.

## API

Available food items:

```http
GET /api/fooditems
GET /api/fooditems?search=pizza
```
