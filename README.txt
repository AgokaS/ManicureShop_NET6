Инструкции по сборке и запуску проекта ManicureShop (.NET 6):

1. Убедитесь, что установлен .NET 6 SDK и Visual Studio 2022 (или VS Code) с поддержкой .NET.
2. Откройте папку /ManicureShop_NET6 в Visual Studio либо выполните в терминале:
   dotnet restore
   dotnet ef migrations add InitialIdentity --context ApplicationDbContext
   dotnet ef database update --context ApplicationDbContext
   dotnet run
3. Приложение использует LocalDB (DefaultConnection) по умолчанию. Если хотите использовать другой SQL Server, измените строку подключения в appsettings.json.
4. Identity (регистрация/вход) подключены через AddDefaultIdentity; страницы находятся в /Areas/Identity после scaffold при необходимости.
5. Для генерации страниц Identity можете использовать scaffolder в Visual Studio: 
   - Правый клик на проект -> Add -> New Scaffolded Item -> Identity
