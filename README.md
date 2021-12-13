# dotnet-templates

This repository contains various .NET templates for Visual Studio.

Issues for the templates should be opened in following repositories:

| Templates | Repository |
|---|---|
|Common project (classlib, console) and item templates|[dotnet/templating](https://github.com/dotnet/templating)|
|ASP.NET and Blazor templates|[dotnet/aspnetcore](https://github.com/dotnet/aspnetcore)|
|WPF templates|[dotnet/wpf](https://github.com/dotnet/wpf)|
|Windows Forms templates|[dotnet/winforms](https://github.com/dotnet/winforms)|
|Test templates|[dotnet/test-templates](https://github.com/dotnet/test-templates)|
|MAUI templates|[dotnet/maui](https://github.com/dotnet/maui)|

## .NET Core

Templates for .NET Core in Visual Studio

## Editorconfig

Templates for creating a default editorconfig in Visual Studio.

## Testing locally

To test this repository run the following from the command-line:
```ini
>git clone https://github.com/dotnet/templates.git
>cd templates
>build.cmd /p:DeployExtension=true
>devenv /rootSuffix Exp
```
