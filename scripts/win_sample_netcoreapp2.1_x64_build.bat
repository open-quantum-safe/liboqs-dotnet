@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=netcoreapp2.1 -c Release -f netcoreapp2.1 -o bin\Release\dotnetOQSSample-netcoreapp2.1-win-x64 -r win-x64 --self-contained
copy x64\oqs.dll bin\Release\dotnetOQSSample-netcoreapp2.1-win-x64