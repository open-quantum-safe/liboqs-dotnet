# Introduction

The Open Quantum Safe project (https://openquantumsafe.org/) supports the development and prototyping of quantum-resistant cryptography. It releases an open-source Open Quantum Safe (OQS) C library implementing various post-quantum cryptographic schemes; it is available from the [liboqs github page](https://github.com/open-quantum-safe/liboqs).

This solution implements a .Net wrapper in C# for the C OQS library. It contains the following projects:
- dotnetOQS: a C# class library targeting .Net core 1.1 wrapping the master branch of the OQS library.
- dotnetOQSUnitTest: unit tests for the dotnetOQS project.
- dotnetOQSSample: a sample program illustrating the usage of the dotnetOQS library.

# Prerequisites

To build the .Net OQS wrapper you need a .Net development environment; see the Getting Started section on the [.Net core](https://dotnet.github.io/) githup page for more information. The wrapper targets version 1.1 of the .Net core, which can be obtained [here](https://dotnet.microsoft.com/download/dotnet-core/1.1).

Builds have been tested on Windows 10 and with Visual Studio 2017 (Community and Enterprise editions).

# OQS installation

The OQS library must be obtained and compiled into a DLL for the target platform before building the solution. The following steps illustrate the process:

1. Download and unzip the OQS [master branch archive](https://github.com/open-quantum-safe/liboqs/archive/master.zip), by default the contents will be into a `liboqs-master` folder.
2. Build the DLL target of the OQS solution, for example:
   `msbuild liboqs-master\VisualStudio\liboqs.sln /p:Configuration=ReleaseDLL;Platform=x64`
3. Copy the OQS DLL into the base dotnetOQS solution directoty, for example:
   `copy liboqs-master\VisualStudio\x64\ReleaseDLL\oqs.dll liboqs-csharp\`

See the [liboqs REAMDE.md](https://github.com/open-quantum-safe/liboqs#building-and-running-on-windows) for more information on building the library on Windows.

# Building dotnetOQS

The dotnetOQS solution can be built using Visual Studio or the command line:
  `msbuild dotnetOQS.sln /p:Configuration=Release`

The sample can be ran using Visual Studio or from the command line:
  `dotnet dotnetOQSSample\bin\Release\netcoreapp1.1\dotnetOQSSample.dll`

# Troubleshooting

If you are experiencing issues building liboqs or the .Net wrapper, opening the solutions with Visual Studio might help revolve issues.
