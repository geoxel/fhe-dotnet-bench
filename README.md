Building a .NET C# interop on TFHE library and benchmarking it.

## Setup

Retrieving the TFHE repo and build it with the C API.
```bash
$ git clone https://github.com/zama-ai/tfhe-rs.git
$ cd tfhe-rs
$ RUSTFLAGS="-C target-cpu=native" cargo +nightly build --release --features=high-level-c-api -p tfhe
$ ls -lF target/release
$ cd ..
```
Retrieve the fhe-dotnet repo and build it.
```bash
$ git clone https://github.com/geoxel/tfhe-cs.git
$ cd tfhe-cs
$ dotnet build -c Release
$ cd ..
```
Retrieve this repo.
```bash
$ git clone https://github.com/geoxel/tfhe-cs-bench.git
```
## Testing
Run the C# test and benchmarking code:
```bash
$ cd tfhe-cs-bench
$ dotnet run -c Release
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
Comparing with WASM and NodeJS native interop goes like this, and shows similar performance improvement as the native NodeJS interop benchmark:
| Benchmark | WASM (ms) | Native NodeJS (ms) | Native C# (ms) |
| ----------- | ----------- | ----------- | ----------- |
| UInt8  | 1.26 | 0.33 | 0.32 |
| UInt32 | 4.99 | 1.29 | 1.30 |

*What a time to be alive!*
