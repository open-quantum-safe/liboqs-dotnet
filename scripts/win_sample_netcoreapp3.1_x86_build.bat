@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:PublishSingleFile=true /p:Platform=x86 /p:TargetFramework=netcoreapp3.1 -c Release -f netcoreapp3.1 -o bin\Release\dotnetOQSSample-netcoreapp3.1-win-x86 -r win-x86 --self-contained
copy x86\oqs.dll bin\Release\dotnetOQSSample-netcoreapp3.1-win-x86