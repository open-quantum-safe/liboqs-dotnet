liboqs-dotnet
=============

[![Build status - Windows](https://ci.appveyor.com/api/projects/status/o3aqcf95kutixle5?svg=true)](https://ci.appveyor.com/project/dstebila/liboqs-dotnet)

---

**liboqs-dotnet** is a .NET wrapper in C# for liboqs, providing quantum-resistant cryptographic algorithms.

Overview
--------

The **Open Quantum Safe (OQS) project** has the goal of developing and prototyping quantum-resistant cryptography.

**liboqs** is an open source C library for quantum-resistant cryptographic algorithms.  See more about liboqs at [https://github.com/open-quantum-safe/liboqs/](https://github.com/open-quantum-safe/liboqs/), including a list of supported algorithms.

**liboqs-dotnet** is an open source .NET wrapper in C# for the liboqs C library for quantum-resistant cryptographic algorithms.  liboqs-dotnet provides:

- a common API for post-quantum key encapsulation mechanisms and digital signature schemes
- a collection of open source implementations of post-quantum cryptography algorithms

The OQS project also provides prototype integrations into application-level protocols to enable testing of quantum-resistant cryptography.

More information on OQS can be found on our website: [https://openquantumsafe.org/](https://openquantumsafe.org/).

liboqs-dotnet
-------------

This solution implements a .NET wrapper in C# for the C OQS library. It contains the following projects:

- **dotnetOQS**: A C# class library, wrapping the master branch of the OQS library.
- **dotnetOQSUnitTest**: Unit tests for the dotnetOQS project.
- **dotnetOQSSample**: A sample program illustrating the usage of the dotnetOQS library.
- **scripts**: Various scripts to build the wrapper on different platforms.

Limitations and security
------------------------

liboqs and liboqs-dotnet are designed for prototyping and evaluating quantum-resistant cryptography.  Security of proposed quantum-resistant algorithms may rapidly change as research advances, and may ultimately be completely insecure against either classical or quantum computers.

We believe that the NIST Post-Quantum Cryptography standardization project is currently the best avenue to identifying potentially quantum-resistant algorithms.  liboqs does not intend to "pick winners", and we strongly recommend that applications and protocols rely on the outcomes of the NIST standardization project when deploying post-quantum cryptography.

We acknowledge that some parties may want to begin deploying post-quantum cryptography prior to the conclusion of the NIST standardization project.  We strongly recommend that any attempts to do make use of so-called **hybrid cryptography**, in which post-quantum public-key algorithms are used alongside traditional public key algorithms (like RSA or elliptic curves) so that the solution is at least no less secure than existing traditional cryptography.

liboqs-dotnet is provided "as is", without warranty of any kind.  See [LICENSE.txt](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/LICENSE.txt) for the full disclaimer.

Building
--------

Builds are tested using the Appveyor continuous integration system on Windows Server 2016 (Visual Studio 2017).  Builds have been tested manually on Windows 10 with Visual Studio 2017 (Community and Enterprise editions), Linux (Ubuntu 18.04 LTS) and macOS Mojave.

### Prerequisites

To build the .NET OQS wrapper you need a .NET development environment; see the Getting Started section on the [.NET Core](https://dotnet.github.io/) GitHub page for more information.

The wrapper targets a minimum of version 1.2 of the .NET standard which supports a wide range of framework listed [here](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support). Version 2.1 is preferred for a smaller build size.

Installing .Net Core 3.1 SDK and above is recommended for the installation steps below which can be obtained [here](https://dotnet.microsoft.com/download/dotnet-core/3.1). (Higher SDK version supports building lower SDK version, though runtime have to be installed separately if you compile it as a framework dependent application.)

To build `liboqs-dotnet` on archnitecture `<arch>` (where `<arch>` is one of the architecture supported by OQS and the dotnet wrapper, for example `x64`, `arm`, etc.), first download or clone this dotnet wrapper into a `liboqs-dotnet` folder, e.g.,

       git clone -b master https://github.com/open-quantum-safe/liboqs-dotnet.git

### Build the OQS dependency

1. Follow the instructions in [liboqs REAMDE.md](https://github.com/open-quantum-safe/liboqs#quickstart) to obtain (in a `liboqs` folder) and build an OQS shared library (on Linux/Mac) or DLL (on Windows) for `<arch>`.

2. Copy the OQS library to the target liboqs-dotnet directory:

       :: For linux
       mkdir liboqs-dotnet/<arch> && cp liboqs/build/lib/liboqs.so liboqs-dotnet/<arch>/
       
       :: For macOS
       mkdir liboqs-dotnet/<arch> && cp liboqs/build/lib/liboqs.dylib liboqs-dotnet/<arch>/

       :: For Windows
       mkdir liboqs-dotnet\<arch> && copy liboqs\VisualStudio\<arch>\ReleaseDLL\oqs.dll liboqs-dotnet\<arch>\

### Building .Net OQS wrapper (optional)

The dotnetOQS wrapper can be built for a specific `<conf>` (`Debug` or `Release`) and target .NET Standard `<vNET>` (e.g., `1.2` or `2.1`) using the command line or Visual Studio (on Windows):

    :: For Windows
    dotnet build dotnetOQS\dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard<vNET> -c <conf> -o bin\<configuration>\dotnetOQS-netstandard<vNET>

    :: For Linux / macOS
    dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard<vNET> -c <conf> -o bin/<configuration>/dotnetOQS-netstandard<vNET>

The resulting wrapper that can be used as a reference in any .NET projects.

You are not required to build the wrapper in order to build the sample application below.

### Building .Net OQS sample application
    
The dotnetOQS sample application can be build using the command line on <OS> (`win` for Windows, `linux` for Linux, `osx` for macOS) or Visual Studio (on Windows):

    :: For Windows
    dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=<arch> /p:TargetFramework=netcoreapp<vNET> -c <conf> -f netcoreapp<vNET> -o bin\<conf>\dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch> -r <OS>-<arch> --self-contained
    copy <arch>\oqs.dll bin\<conf>\dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch>
    
    :: For Linux / macOS
    dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:Platform=<arch> /p:TargetFramework=netcoreapp<vNET> -c <conf> -f netcoreapp<vNET> -o bin/<conf>/dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch> -r <OS>-<arch> --self-contained
    cp <arch>/liboqs.* bin/<conf>/dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch>

You can cross compile for different operating system using the [dotnet publish cli command](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21).

See the `scripts` folder for more compilation samples.

### Running the sample and test programs

The sample program can be ran using the command line or Visual Studio (on Windows):

    :: For Windows (netcoreapp1.1)
    dotnet bin\<conf>\dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch>.dll
    
    :: For Windows (netcoreapp2.1 and above)
    .\bin\<conf>\dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch>\dotnetOQSSample.exe
    
    :: For Linux / macOS (netcoreapp1.1)
    dotnet bin/<conf>/dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch>.dll
    
    :: For Linux / macOS (netcoreapp2.1 and above)
    ./bin/<conf>/dotnetOQSSample-netcoreapp<vNET>-<OS>-<arch>/dotnetOQSSample

The unit tests can be run using Visual Studio's "Test" menu. It is currently not possible to use `dotnet test` on Linux to execute the test case.

Troubleshooting
---------------

If you are experiencing issues building liboqs or the .NET wrapper on the command line, opening the solutions with Visual Studio might help revolve issues.

License
-------

liboqs-dotnet is licensed under the MIT License; see [LICENSE.txt](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/LICENSE.txt) for details.

Team
----

The Open Quantum Safe project is led by [Douglas Stebila](https://www.douglas.stebila.ca/research/) and [Michele Mosca](http://faculty.iqc.uwaterloo.ca/mmosca/) at the University of Waterloo.

Contributors to the liboqs-dotnet wrapper include:
 - Christian Paquin (Microsoft Research)
 - Yong Jian Ming

### Support

Financial support for the development of Open Quantum Safe has been provided by Amazon Web Services and the Tutte Institute for Mathematics and Computing.

We'd like to make a special acknowledgement to the companies who have dedicated programmer time to contribute source code to OQS, including Amazon Web Services, evolutionQ, and Microsoft Research.

Research projects which developed specific components of OQS have been supported by various research grants, including funding from the Natural Sciences and Engineering Research Council of Canada (NSERC); see the source papers for funding acknowledgments.
