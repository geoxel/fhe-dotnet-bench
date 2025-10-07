using System;
using System.Runtime.InteropServices;

namespace Fhe;

internal static partial class SafeNativeMethods
{
#if OS_WINDOWS
    private const string LibraryPrefix = "";
    private const string LibraryExtension = ".dll";
#elif OS_LINUX
    private const string LibraryPrefix = "lib";
    private const string LibraryExtension = ".so";
#elif OS_MACOS
    private const string LibraryPrefix = "lib";
    private const string LibraryExtension = ".dylib";
#else
    #error Unsupported platform
#endif

    private const string LibraryPath = "tfhe-dotnet-lib/build/" + LibraryPrefix + "tfhe-dotnet" + LibraryExtension;

    [LibraryImport(LibraryPath)]
    public static partial int GenerateKeys(out nint client_key, out nint server_key);

    [LibraryImport(LibraryPath)]
    public static partial int ClientKey_Destroy(nint client_key);

    [LibraryImport(LibraryPath)]
    public static partial int ServerKey_Destroy(nint server_key);

    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicBuffer
    {
        public nint pointer;
        public nint length;
        public nint destructor;
    };

    [LibraryImport(LibraryPath)]
    public static partial int DynamicBuffer_Destroy(ref DynamicBuffer buffer);

    // UInt8

    [LibraryImport(LibraryPath)]
    public static partial int UInt8_Encrypt(nint client_key, byte value, out nint fhe);

    [LibraryImport(LibraryPath)]
    public static partial int UInt8_Decrypt(nint client_key, nint fhe, out byte out_value);

    [LibraryImport(LibraryPath)]
    public static partial int UInt8_Destroy(nint value);

    [LibraryImport(LibraryPath)]
    public static partial int UInt8_Add(nint value1, nint value2, out nint out_value);

    [LibraryImport(LibraryPath)]
    public static partial int UInt8_Serialize(nint value, out DynamicBuffer out_buffer);

    [LibraryImport(LibraryPath)]
    public static partial int UInt8_Deserialize(nint ptr, int length, out nint out_value);

    // UInt32

    [LibraryImport(LibraryPath)]
    public static partial int UInt32_Encrypt(nint client_key, uint value, out nint fhe);

    [LibraryImport(LibraryPath)]
    public static partial int UInt32_Decrypt(nint client_key, nint fhe, out uint out_value);

    [LibraryImport(LibraryPath)]
    public static partial int UInt32_Destroy(nint value);

    [LibraryImport(LibraryPath)]
    public static partial int UInt32_Add(nint value1, nint value2, out nint out_value);

    [LibraryImport(LibraryPath)]
    public static partial int UInt32_Serialize(nint value, out DynamicBuffer out_buffer);

    [LibraryImport(LibraryPath)]
    public static partial int UInt32_Deserialize(nint ptr, int length, out nint out_value);
}
