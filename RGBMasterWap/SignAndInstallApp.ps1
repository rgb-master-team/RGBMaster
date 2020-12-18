#Requires -RunAsAdministrator

$ErrorActionPreference = "Stop"

Write-Output "Installing rgbmaster certificate....."

Import-Certificate -FilePath "RGBMasterWap_2.4.0.0_x64.cer" -CertStoreLocation Cert:\LocalMachine\TrustedAppRoot

Write-Output "rgbmaster certificate installed successfully."
Write-Output "Installing actual RGBMaster app-bundle....."

$package = Get-AppPackage -Name "6442bda9-8e61-4e00-a286-4af1d801567a"

if ($package -ne $null) {
    $packageFullname = $package.PackageFullName
    Write-Output "Found older RGBMaster package by the full-name of " $packageFullname
    Write-Output "Uninstalling old package..."
    Remove-AppPackage -Package $packageFullname
}

Add-AppPackage -path "RGBMasterWap_2.4.0.0_x64.msixbundle"