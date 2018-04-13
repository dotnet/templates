@echo off
powershell -ExecutionPolicy ByPass %~dp0build\Build.ps1 -build /p:Solution=Templates.sln
exit /b %ErrorLevel%
