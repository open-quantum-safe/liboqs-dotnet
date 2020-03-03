@echo off

dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard1.2 -c Debug -o bin\Debug\dotnetOQS-netstandard1.2
dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard2.0 -c Debug -o bin\Debug\dotnetOQS-netstandard2.0

dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard1.2 -c Release -o bin\Release\dotnetOQS-netstandard1.2
dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard2.0 -c Release -o bin\Release\dotnetOQS-netstandard2.0