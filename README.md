# FileDex

FileDex is a small WPF application for viewing and inspecting file metadata.

Features
- Open a file and view extracted metadata entries
- Search metadata by key or value
- Copy individual metadata values to the clipboard
- Shows file size and entry count in the status bar

Requirements
- .NET 10 SDK
- Visual Studio 2026 (or any IDE that supports WPF on .NET 10)

Build and run
- From Visual Studio: open FileDex.slnx and press F5
- From the command line (PowerShell):
  - dotnet build FileDex/FileDex.csproj
  - dotnet run --project FileDex/FileDex.csproj

Project structure
- FileDex/Views: WPF XAML views
- FileDex/ViewModels: MVVM view models and commands
- FileDex/Services: metadata reader interfaces and implementations
- FileDex/Models: domain models

License
This repository does not include an explicit license file. Check the project repository for license information before reusing the code.

