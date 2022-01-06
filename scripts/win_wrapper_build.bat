@echo off

dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f net6.0 -c Debug -o bin\Debug\dotnetOQS-net6.0

dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f net6.0 -c Release -o bin\Release\dotnetOQS-net6.0
