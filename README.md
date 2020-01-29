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

- **dotnetOQS**: A C# class library targeting .NET Standard 1.2 and 2.0, wrapping the master branch of the OQS library.
- **dotnetOQSUnitTest**: Unit tests for the dotnetOQS project.
- **dotnetOQSSample**: A sample program illustrating the usage of the dotnetOQS library.

Limitations and security
------------------------

liboqs and liboqs-dotnet are designed for prototyping and evaluating quantum-resistant cryptography.  Security of proposed quantum-resistant algorithms may rapidly change as research advances, and may ultimately be completely insecure against either classical or quantum computers.

We believe that the NIST Post-Quantum Cryptography standardization project is currently the best avenue to identifying potentially quantum-resistant algorithms.  liboqs does not intend to "pick winners", and we strongly recommend that applications and protocols rely on the outcomes of the NIST standardization project when deploying post-quantum cryptography.

We acknowledge that some parties may want to begin deploying post-quantum cryptography prior to the conclusion of the NIST standardization project.  We strongly recommend that any attempts to do make use of so-called **hybrid cryptography**, in which post-quantum public-key algorithms are used alongside traditional public key algorithms (like RSA or elliptic curves) so that the solution is at least no less secure than existing traditional cryptography.

liboqs-dotnet is provided "as is", without warranty of any kind.  See [LICENSE.txt](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/LICENSE.txt) for the full disclaimer.

Building
--------

Builds are tested using the Appveyor continuous integration system on Windows Server 2016 (Visual Studio 2017).  Builds have been tested manually on Windows 10 with Visual Studio 2017 (Community and Enterprise editions) and macOS Mojave.

### Step 0: Prerequisites

To build the .NET OQS wrapper you need a .NET development environment; see the Getting Started section on the [.NET Core](https://dotnet.github.io/) GitHub page for more information.

The wrapper targets version 1.2 and 2.0 of the .NET standard framework which supports a wide range of framework [here](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support).

Installing .Net Core 2.1 SDK and above is recommended for the installation steps below which can be obtained [here](https://dotnet.microsoft.com/download/dotnet-core/2.1).

### Step 1: Build liboqs

#### Linux/macOS

1. Follow the instructions in [liboqs REAMDE.md](https://github.com/open-quantum-safe/liboqs#linuxmacos) to build a dynamic/shared library.
2. Goto liboqs-dotnet and create "x64" and "arm" folder:

        cd ../liboqs-dotnet
        mkdir x64    (Required)
        mkdir arm    (Optional: For arm support only)
        
3. Copy the output into the base dotnetOQS solution directory:

        cd ../liboqs
        cp .libs/liboqs.so ../liboqs-dotnet/x64/liboqs.dylib       (For Linux)
        cp .libs/liboqs.dylib ../liboqs-dotnet/x64/liboqs.dylib    (For macOS)

The folder to copy into is based on the architecture you are building for.

#### Windows

The master branch of the OQS library must be obtained and compiled into a DLL for the target platform before building the liboqs-dotnet solution.

1. Download and unzip the [liboqs master branch archive](https://github.com/open-quantum-safe/liboqs/archive/master.zip). By default the contents will be into a `liboqs-master` folder.
2. Build the DLL target of the OQS solution, either using Visual Studio or on the command line:

		msbuild liboqs-master\VisualStudio\liboqs.sln /p:Configuration=ReleaseDLL;Platform=x64

3. Copy the liboqs DLL into the base dotnetOQS solution directory:

		copy liboqs-master\VisualStudio\x64\ReleaseDLL\oqs.dll liboqs-dotnet\x64

The folder to copy into is based on the architecture you are building for.

See the [liboqs REAMDE.md](https://github.com/open-quantum-safe/liboqs#building-and-running-on-windows) for more information on building the library on Windows.

### Step 2: Build dotnetOQS

#### Building .Net OQS wrapper

The dotnetOQS wrapper can be built using Visual Studio or on the command line:

    :: This is for Debugging purpose only.
	dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard1.2 -c Debug -o bin/Debug/dotnetOQS-netstandard1.2
    dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard2.0 -c Debug -o bin/Debug/dotnetOQS-netstandard2.0
    
    :: Prefer release build for optimization :)
    dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard1.2 -c Release -o bin/Release/dotnetOQS-netstandard1.2
    dotnet build dotnetOQS/dotnetOQS.csproj /p:Platform=AnyCPU -f netstandard2.0 -c Release -o bin/Release/dotnetOQS-netstandard2.0

The command lines above build the wrapper that can be used for reference in any .NET projects.

For your convenience, you could execute the scripts in the root liboqs-dotnet folder which does the same thing above:
    
See [win_wrapper_build.bat](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/scripts/win_wrapper_build.bat) or [linux_macos_wrapper_build.sh](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/scripts/linux_macos_wrapper_build.sh)

#### Building .Net OQS sample application
    
The dotnetOQS sample application can be build using Visual Studio or on the command line:

    :: For building via Windows
    dotnet publish dotnetOQSSample\dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=netcoreapp2.1 -c Release -f netcoreapp2.1 -o bin\Release\dotnetOQSSample-netcoreapp2.1-win-x64 -r win-x64 --self-contained
    copy x64\oqs.dll bin\Release\dotnetOQSSample-netcoreapp2.1-win-x64
    
    :: For building via macOS
    dotnet publish dotnetOQSSample/dotnetOQSSample.csproj /p:Platform=x64 /p:TargetFramework=netcoreapp2.1 -c Release -f netcoreapp2.1 -o bin/Release/dotnetOQSSample-netcoreapp2.1-osx-x64 -r osx-x64 --self-contained
    cp x64/liboqs.dylib bin/Release/dotnetOQSSample-netcoreapp2.1-osx-x64

You can build for other architecture and platform by using the [dotnet publish cli command](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21).

### Running the sample and test programs

The sample program can be ran using Visual Studio or on the command line:

    :: For windows (netcoreapp1.1)
    dotnet bin/Release/dotnetOQSSample-netcoreapp1.1-win-x64.dll
    
    :: For windows (netcoreapp2.1 and above)
    ./bin/Release/dotnetOQSSample-netcoreapp2.1-win-x64/dotnetOQSSample.exe
    
    :: For linux (netcoreapp1.1)
    dotnet bin/Release/dotnetOQSSample-netcoreapp1.1-linux-x64.dll
    
    :: For linux (netcoreapp2.1 and above)
    ./bin/Release/dotnetOQSSample-netcoreapp2.1-linux-x64/dotnetOQSSample
    
    :: For macOS (netcoreapp1.1)
    dotnet bin/Release/dotnetOQSSample-netcoreapp1.1-osx-x64.dll
    
    :: For macOS (netcoreapp2.1 and above)
    ./bin/Release/dotnetOQSSample-netcoreapp2.1-osx-x64/dotnetOQSSample
    
    :: For running tests.
    :: You will need to manually copy the oqs.dll (For windows) liboqs.so (For linux) liboqs.dylib (For macOS) into "dotnetOQSUnitTest/bin/Debug/netcoreapp1.1".
	dotnet test

The unit tests can be running using Visual Studio's "Test" menu.

### Extra

There are some sample scripts that you can execute in the scripts folder. You are free to play around with it if you want a custom build.

Troubleshooting
---------------

If you are experiencing issues building liboqs or the .NET wrapper on the command line, opening the solutions with Visual Studio might help revolve issues.

License
-------

liboqs-dotnet is licensed under the MIT License; see [LICENSE.txt](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/LICENSE.txt) for details.

Team
----

The Open Quantum Safe project is led by [Douglas Stebila](https://www.douglas.stebila.ca/research/) and [Michele Mosca](http://faculty.iqc.uwaterloo.ca/mmosca/) at the University of Waterloo.

liboqs-dotnet was developed by [Christian Paquin](https://www.microsoft.com/en-us/research/people/cpaquin/) at Microsoft Research.

### Support

Financial support for the development of Open Quantum Safe has been provided by Amazon Web Services and the Tutte Institute for Mathematics and Computing.

We'd like to make a special acknowledgement to the companies who have dedicated programmer time to contribute source code to OQS, including Amazon Web Services, evolutionQ, and Microsoft Research.

Research projects which developed specific components of OQS have been supported by various research grants, including funding from the Natural Sciences and Engineering Research Council of Canada (NSERC); see the source papers for funding acknowledgments.
