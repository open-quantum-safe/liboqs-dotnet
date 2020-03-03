@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=x86 /p:TargetFramework=netcoreapp1.1 -c Release -f netcoreapp1.1 -o bin\Release\dotnetOQSSample-netcoreapp1.1-win-x86
copy x86\oqs.dll bin\Release\dotnetOQSSample-netcoreapp1.1-win-x86