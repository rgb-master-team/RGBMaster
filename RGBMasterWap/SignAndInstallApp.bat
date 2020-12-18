cd %~dp0

powershell -ExecutionPolicy Bypass -File "%~dp0\SignAndInstallApp.ps1"

@pause