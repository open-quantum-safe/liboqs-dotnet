#!/bin/bash
dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:PublishSingleFile=true /p:Platform=x64 /p:TargetFramework=netcoreapp3.1 -c Release -f netcoreapp3.1 -o bin/Release/dotnetOQSSample-netcoreapp3.1-linux-x64 -r linux-x64 --self-contained
cp x64/liboqs.so bin/Release/dotnetOQSSample-netcoreapp3.1-linux-x64