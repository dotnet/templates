@echo off
powershell -ExecutionPolicy ByPass  %~dp0Build.ps1 -restore -build -sign -deploy -ci %*
exit /b %ErrorLevel%
