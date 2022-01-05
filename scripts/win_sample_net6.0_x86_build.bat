@echo off

dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=x86 /p:TargetFramework=net6.0 -c Release -f net6.0 -o bin\Release\dotnetOQSSample-net6.0-win-x86 -r win-x86 --self-contained
copy x86\oqs.dll bin\Release\dotnetOQSSample-net6.0-win-x86
