#Requires -RunAsAdministrator

$ErrorActionPreference = "Stop"

Write-Output "Installing rgbmaster certificate....."

Import-Certificate -FilePath "RGBMasterWap_2.1.0.0_x64.cer" -CertStoreLocation Cert:\LocalMachine\TrustedAppRoot

Write-Output "rgbmaster certificate installed successfully."
Write-Output "Installing actual RGBMaster app-bundle....."

Add-AppPackage -path "RGBMasterWap_2.1.0.0_x64.msixbundle"