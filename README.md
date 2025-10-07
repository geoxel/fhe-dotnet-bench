Building a native TFHE library for .NET C# interop and benchmarking it.

## Setup

Retrieving the TFHE repo and build it with the C API.
```bash
$ git clone https://github.com/zama-ai/tfhe-rs.git
$ cd tfhe-rs
$ RUSTFLAGS="-C target-cpu=native" cargo +nightly build --release --features=high-level-c-api -p tfhe
$ ls -lF target/release
$ cd ..
```
Retrieve this repo.
```bash
$ git clone https://github.com/geoxel/fhe-native-and-csharp.git
```
## Build
Build the native interop library.
```bash
$ cd fhe-native-and-csharp/tfhe-dotnet-lib
$ mkdir build && cd build
$ cmake .. && cmake --build .
-- The C compiler identification is AppleClang 17.0.0.17000013
-- The CXX compiler identification is AppleClang 17.0.0.17000013
-- Detecting C compiler ABI info
-- Detecting C compiler ABI info - done
-- Check for working C compiler: /Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/cc - skipped
-- Detecting C compile features
-- Detecting C compile features - done
-- Detecting CXX compiler ABI info
-- Detecting CXX compiler ABI info - done
-- Check for working CXX compiler: /Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ - skipped
-- Detecting CXX compile features
-- Detecting CXX compile features - done
-- Configuring done
-- Generating done
-- Build files have been written to: /Users/kepler/zama/fhe-native-and-csharp/tfhe-dotnet-lib/build
[ 50%] Building CXX object CMakeFiles/tfhe-dotnet.dir/lib.cpp.o
[100%] Linking CXX shared library libtfhe-dotnet.dylib
[100%] Built target tfhe-dotnet
```
Build the .NET test:
```bash
$ cd ../..
$ dotnet build -c Release
```
## Testing
Run the C# test and benchmarking code:
```bash
$ dotnet build -c Release
Generate keys...
Encrypting...
Serialized length: 263448, deserialized value: 42
Adding...
Decrypting...
a + b = 60
Benchmarking...
FheUInt8 : 0.32 ms
FheUInt32: 1.30 ms
All done
$ uname -a
Darwin youpi.local 25.0.0 Darwin Kernel Version 25.0.0: Mon Aug 25 21:17:45 PDT 2025; root:xnu-12377.1.9~3/RELEASE_ARM64_T8103 arm64 arm64 Macmini9,1 Darwin
```
Comparing with WASM and NodeJS native interop goes like this, and showa similar performance improvement as the native NodeJS interop benchmark:
| Benchmark | WASM (ms) | Native NodeJS (ms) | Native C# (ms) |
| ----------- | ----------- | ----------- | ----------- |
| UInt8  | 1.26 | 0.33 | 0.32 |
| UInt32 | 4.99 | 1.29 | 1.30 |

*What a time to be alive!*
