@echo off
powershell -ExecutionPolicy ByPass %~dp0build\Build.ps1 -build %*
exit /b %ErrorLevel%
