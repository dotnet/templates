@echo off
powershell -ExecutionPolicy ByPass %~dp0build\Build.ps1 -restore -build -deploy -pack /p:Solution=Templates.sln
exit /b %ErrorLevel%
