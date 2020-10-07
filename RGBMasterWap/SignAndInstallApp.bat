REM certutil -addstore -f "TrustedAppRoot" "%~dp0\RGBMasterWap_2.0.0.0_x64.cer"

cd %~dp0

powershell -ExecutionPolicy Bypass -File "%~dp0\SignAndInstallApp.ps1"

@pause