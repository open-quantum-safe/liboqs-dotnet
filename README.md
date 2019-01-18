# Introduction

The Open Quantum Safe project (https://openquantumsafe.org/) supports the development and prototyping of quantum-resistant cryptography. It releases an open-source Open Quantum Safe (OQS) C library implementing various post-quantum cryptographic schemes; it is available from the [liboqs github page](https://github.com/open-quantum-safe/liboqs).

This solution implements a .Net wrapper in C# for the C OQS library. It contains the following projects:
- dotnetOQS: a C# class library targeting .Net core 1.1 wrapping the master branch of the OQS library.
- dotnetOQSUnitTest: unit tests for the dotnetOQS project.
- dotnetOQSSample: a sample program illustrating the usage of the dotnetOQS library.

# OQS installation

The OQS library must be obtained and compiled into a DLL for the target platform before building the solution. The following steps illustrate the process:

1. Download and unzip the OQS [master branch archive](https://github.com/open-quantum-safe/liboqs/archive/master.zip), by default the contents will be into a `liboqs-master` folder.
2. Build the DLL target of the OQS solution, for example:
   `msbuild liboqs-master\VisualStudio\liboqs.sln /p:Configuration=ReleaseDLL;Platform=x64`
3. Copy the OQS DLL into the base dotnetOQS solution directoty:
   `copy liboqs-master\VisualStudio\x64\ReleaseDLL\oqs.dll dotnetOQS\`

# Building dotnetOQS

The dotnetOQS solution can be built using Visual Studio or the command line:
  `msbuild dotnetOQS.sln /p:Configuration=Release`

The sample can be ran using Visual Studio or from the command line:
  `dotnet dotnetOQSSample\bin\Release\netcoreapp1.1\dotnetOQSSample.dll`


