@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:PublishSingleFile=true /p:Platform=x64 /p:TargetFramework=netcoreapp3.1 -c Release -f netcoreapp3.1 -o bin\Release\dotnetOQSSample-netcoreapp3.1-win-x64 -r win-x64 --self-contained
copy x64\oqs.dll bin\Release\dotnetOQSSample-netcoreapp3.1-win-x64