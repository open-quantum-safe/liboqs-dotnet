#!/bin/bash
dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard2.1 -c Debug -o bin/Debug/dotnetOQS-netstandard2.1
dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard2.1 -c Release -o bin/Release/dotnetOQS-netstandard2.1
