@echo off
powershell -ExecutionPolicy ByPass  %~dp0Build.ps1 -restore -build -pack -sign -ci %*
exit /b %ErrorLevel%
