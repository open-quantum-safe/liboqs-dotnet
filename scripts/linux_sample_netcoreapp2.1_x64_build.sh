#!/bin/bash
dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=netcoreapp2.1 -c Release -f netcoreapp2.1 -o bin/Release/dotnetOQSSample-netcoreapp2.1-linux-x64 -r linux-x64 --self-contained
cp x64/liboqs.so bin/Release/dotnetOQSSample-netcoreapp2.1-linux-x64