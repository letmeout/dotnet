# Learn Dot Net Core Topics

- Background Task
    - IHostService
    - BackgroundService
    - WorkerService
    - Hangfire
    - Cloud options

- Razor Pages
  - EF's migration feature
    - For PowerShell
      - `Add-Migration InitialCreate`
      - `Update-Database`
    - .NET CLI
      - `dotnet tool install --global dotnet-ef`
      - `dotnet ef migrations add InitialCreate`
      - `dotnet ef migrations add Migration -c DbContext -o Migrations/FolderPath`
      - `dotnet ef database update`
      - 