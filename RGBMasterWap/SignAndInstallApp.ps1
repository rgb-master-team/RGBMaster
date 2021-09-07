#Requires -RunAsAdministrator

$ErrorActionPreference = "Stop"

$WindowsDeveloperLicense = Get-WindowsDeveloperLicense

Write-Output $WindowsDeveloperLicense.IsValid

# Check for developer mode
if ($WindowsDeveloperLicense.IsValid -ne $True)
{
    Write-Output "Please enable Developer Mode in order to install the app."
    Exit
}

Write-Output "Developer mode is enabled."

$rgbMasterVersion = "2.4.0.0"

Write-Output "Installing rgbmaster certificate....."

Import-Certificate -FilePath "RGBMasterWap_$($rgbMasterVersion)_x64.cer" -CertStoreLocation Cert:\LocalMachine\TrustedAppRoot

Write-Output "rgbmaster certificate installed successfully."
Write-Output "Installing actual RGBMaster app-bundle....."

$package = Get-AppPackage -Name "6442bda9-8e61-4e00-a286-4af1d801567a"

if ($package -ne $null) {
    $packageFullname = $package.PackageFullName
    Write-Output "Found older RGBMaster package by the full-name of " $packageFullname
    Write-Output "Uninstalling old package..."
    Remove-AppPackage -Package $packageFullname
}

Add-AppPackage -path "RGBMasterWap_$($rgbMasterVersion)_x64.msixbundle"