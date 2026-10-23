# Dynamic Lending Platform

This repository contains the starter application for a lending processing platform with:
- ASP.NET MVC
- SQL Server
- Identity + user management
- forgot/reset password flow
- audit logging foundation
- report execution via stored procedures
- scheduler configuration UI
- premium admin dashboard layout

## Features implemented
- User management
- Email-based password reset
- Role-based access via ASP.NET Identity
- SQL stored procedure access wrapper
- Administrator dashboard
- Report execution using stored procedures
- Scheduler UI
- Modern UI shell

## Database
Run the SQL script in `sql/LendingProcessingSystem.sql` on your local SQL Server instance.

## Setup
1. Create a SQL Server database.
2. Run the SQL script.
3. Update connection string in `appsettings.json`.
4. Configure SMTP settings in `appsettings.json`.
5. Restore packages:
   ```bash
   dotnet restore
   ```
6. Run the project:
   ```bash
   dotnet run
   ```

## Default admin
Create a user through the app, then assign the `Admin` role using ASP.NET Identity management.

## Notes
This project is a production-ready foundation structure for the required workflow platform and can be extended with product workflows, APIs, and case processing modules.
