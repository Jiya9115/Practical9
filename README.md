# Practical 9 - Placement Dashboard using Data Controls and an Alternative to Crystal Reports

**Subject:** 01CE1523 - .NET Technologies

A placement dashboard built with **ASP.NET Core MVC**, **ADO.NET** (`Microsoft.Data.SqlClient`) and **SQL Server**.
It shows summary cards, a filterable placement table, a company-wise placement count and a printable report
that can be saved as a PDF - without Crystal Reports.

## Features

- Dashboard cards: total students, students placed, companies, average package (LPA)
- Placement records table bound to SQL data with a status filter (All / Placed / In Process / Not Placed)
- Company-wise placement count (LEFT JOIN + GROUP BY)
- Printable report page - use **Print -> Save as PDF** in the browser

## Alternative to Crystal Reports

The report is a plain HTML page (`Views/Placement/Report.cshtml`) with print-specific CSS.
It needs no Crystal Reports runtime, no designer and no extra NuGet package.
Click **Print / Save as PDF** and choose **Save as PDF** in the print dialog.

## Project structure

```
PlacementDashboard/
|-- 1_Database/          SQL scripts (database, tables + sample data, report queries)
|-- 2_Connection/        DatabaseConnection.cs, ConnectionStrings.txt
|-- 3_Dashboard/         PlacementRepository.cs and Models/
|-- Controllers/         PlacementController.cs
|-- Views/               Placement/Index, Placement/Report, Shared/_Layout, Shared/Error
|-- wwwroot/css/         site.css
|-- Properties/          launchSettings.json
|-- appsettings.json
|-- Program.cs
`-- PlacementDashboard.csproj
```

## Requirements

- Visual Studio 2022 or newer with the *ASP.NET and web development* workload
- .NET 8 SDK or newer
- SQL Server LocalDB, SQL Server Express or SQL Server

## How to run

1. Open `PlacementDashboard/1_Database/01_CreateDatabase.sql` in SSMS / SQL Server Object Explorer and execute it.
2. Execute `PlacementDashboard/1_Database/02_CreateTablesAndSampleData.sql`.
3. Check `PlacementDashboard/appsettings.json`. Default is `(localdb)\MSSQLLocalDB`.
   Other options are listed in `2_Connection/ConnectionStrings.txt`.
4. Open `PlacementDashboard/PlacementDashboard.csproj` in Visual Studio.
5. Restore NuGet packages, build and run (Ctrl+F5).
6. Use the status filter, then click **Open Printable Report / Save as PDF**.

## Screenshots

Add your output screenshots here (dashboard, printable report, save-as-PDF dialog).
