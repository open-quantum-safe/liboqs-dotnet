#!/bin/bash
dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:PublishSingleFile=true /p:Platform=x64 /p:TargetFramework=netcoreapp3.1 -c Release -f netcoreapp3.1 -o bin/Release/dotnetOQSSample-netcoreapp3.1-osx-x64 -r osx-x64 --self-contained
cp x64/liboqs.dylib bin/Release/dotnetOQSSample-netcoreapp3.1-osx-x64