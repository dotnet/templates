@echo off
powershell -ExecutionPolicy ByPass %~dp0build\Build.ps1 -restore -build -deploy -pack -log %*
exit /b %ErrorLevel%
