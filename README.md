# ВелосипедДрайв — ДЭ 09.02.07-2-2026, вариант 2, модули 1–2

- `Database/BikeDrive.sql` — скрипт БД MS SQL Server (схема в 3НФ + импортированные данные).
- `BikeDrive/` — исходный код приложения (WPF, .NET 8, Entity Framework Core).
- `Release/` — исполняемые файлы (framework-dependent, нужен .NET 8 Desktop Runtime).
- `Docs/` — ER-диаграмма (PDF), блок-схема по ГОСТ 19.701-90 (PDF), отчёт со скриншотами.

## Запуск
1. Выполнить `Database/BikeDrive.sql` на SQL Server.
2. В `BikeDrive/appsettings.json` (или `Release/appsettings.json`) указать строку подключения.
3. Запустить `BikeDrive.exe`.
