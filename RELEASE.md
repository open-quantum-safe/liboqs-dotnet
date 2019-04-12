liboqs-dotnet version 0.1.0
===========================

About
-----

The **Open Quantum Safe (OQS) project** has the goal of developing and prototyping quantum-resistant cryptography.  More information on OQS can be found on our website: https://openquantumsafe.org/ and on Github at https://github.com/open-quantum-safe/.  

**liboqs** is an open source C library for quantum-resistant cryptographic algorithms.  See more about liboqs at [https://github.com/open-quantum-safe/liboqs/](https://github.com/open-quantum-safe/liboqs/), including a list of supported algorithms.

**liboqs-dotnet** is an open source .NET wrapper in C# for the liboqs C library for quantum-resistant cryptographic algorithms.  Details about liboqs-dotnet can be found in [README.md](https://github.com/open-quantum-safe/liboqs-dotnet/blob/master/README.md).  See in particular limitations on intended use.

Release notes
=============

This release of liboqs-dotnet was released on <span style="color: red;">TODO</span>.  Its release page on GitHub is https://github.com/open-quantum-safe/liboqs-dotnet/releases/tag/0.1.0.

What's New
----------

This is the first release of liboqs-dotnet.

This solution implements a .NET wrapper in C# for the C OQS library. It contains the following projects:

- **dotnetOQS**: A C# class library targeting .NET Core 1.1, wrapping the master branch of the OQS library.
- **dotnetOQSUnitTest**: Unit tests for the dotnetOQS project.
- **dotnetOQSSample**: A sample program illustrating the usage of the dotnetOQS library.

liboqs-dotnet can be compiled against liboqs master branch, and makes available all digital signature schemes and key encapsulation mechanisms from liboqs.
