#!/bin/bash
dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=net6.0 -c Release -f net6.0 -o bin/Release/dotnetOQSSample-net6.0-linux-x64 -r linux-x64 --self-contained
cp x64/liboqs.so bin/Release/dotnetOQSSample-net6.0-linux-x64
