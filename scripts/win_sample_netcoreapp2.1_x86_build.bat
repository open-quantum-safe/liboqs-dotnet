@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=x86 /p:TargetFramework=netcoreapp2.1 -c Release -f netcoreapp2.1 -o bin\Release\dotnetOQSSample-netcoreapp2.1-win-x86 -r win-x86 --self-contained
copy x86\oqs.dll bin\Release\dotnetOQSSample-netcoreapp2.1-win-x86