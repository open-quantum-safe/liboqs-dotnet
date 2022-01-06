@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=net6.0 -c Release -f net6.0 -o bin\Release\dotnetOQSSample-net6.0-win-x64 -r win-x64 --self-contained
copy x64\oqs.dll bin\Release\dotnetOQSSample-net6.0-win-x64
