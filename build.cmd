@echo off
powershell -ExecutionPolicy ByPass %~dp0build\Build.ps1 -restore -build -deploy -pack %*
exit /b %ErrorLevel%
