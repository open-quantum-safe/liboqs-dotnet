param (
    [Parameter(Mandatory=$true)][string]$targetDir
)
$editbinPath = (& cmd /c "where editbin.exe")
if (!$editbinPath) {
    # read registry default value
    $devenvPath = (Get-ItemProperty -Path "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\devenv.exe" -ErrorAction SilentlyContinue)."(default)"
    if (!$devenvPath) {
        Write-Host "Failed to find Visaul Studio installled path"
        exit 1
    }
    $searchRoot = [regex]::Match($devenvPath.Trim('"'), "^.+\\20[0-9]{2}\\").Value
    if (!$searchRoot) {
        Write-Host "Unrecognized Visual Studio installled path - $devenvPath"
        exit 1
    }
    $searchRoot
    $editbinPath = (Get-ChildItem -Path $searchRoot -Filter editbin.exe -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1).FullName
    $editbinPath 
    if (!$editbinPath) {
        Write-Host "Failed to find editbin.exe"
        exit 1
    }
}
& $editbinPath /STACK:50000000 $(Join-Path $targetDir "testhost.exe")

