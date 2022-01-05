#!/bin/bash
dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=net6.0 -c Release -f net6.0 -o bin/Release/dotnetOQSSample-net6.0-osx-x64 -r osx-x64 --self-contained
cp x64/liboqs.dylib bin/Release/dotnetOQSSample-net6.0-osx-x64
