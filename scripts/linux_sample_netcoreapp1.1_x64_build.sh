#!/bin/bash
dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=netcoreapp1.1 -c Release -f netcoreapp1.1 -o bin/Release/dotnetOQSSample-netcoreapp1.1-linux-x64
cp x64/liboqs.so bin/Release/dotnetOQSSample-netcoreapp1.1-linux-x64