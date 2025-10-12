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

    private const string LibraryPath = "../tfhe-rs/target/release/" + LibraryPrefix + "tfhe" + LibraryExtension;

    [LibraryImport(LibraryPath, EntryPoint = "config_builder_default")]
    public static partial int ConfigBuilderDefault(out nint config_builder);

    [LibraryImport(LibraryPath, EntryPoint = "config_builder_build")]
    public static partial int ConfigBuilderBuild(nint config_builder, out nint config);

    [LibraryImport(LibraryPath, EntryPoint = "generate_keys")]
    public static partial int GenerateKeys(nint config, out nint client_key, out nint server_key);

    [LibraryImport(LibraryPath, EntryPoint = "set_server_key")]
    public static partial int SetServerKey(nint server_key);

    [LibraryImport(LibraryPath, EntryPoint = "client_key_destroy")]
    public static partial int ClientKey_Destroy(nint client_key);

    [LibraryImport(LibraryPath, EntryPoint = "server_key_destroy")]
    public static partial int ServerKey_Destroy(nint server_key);

    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicBuffer
    {
        public nint pointer;
        public nint length;
        public nint destructor;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicBufferView
    {
        public nint pointer;
        public nint length;
    };

    internal static byte[] DynamicBuffer_ToArray(DynamicBuffer buffer)
    {
        try
        {
            const int MaxArraySize = 0x7FFFFFC7;
            if (buffer.length > MaxArraySize)
                throw new FheException(1); // TODO: use a better error code

            var result = new byte[(int)buffer.length];
            Marshal.Copy(buffer.pointer, result, 0, result.Length);
            return result;
        }
        finally
        {
            DynamicBuffer_Destroy(ref buffer);
        }
    }

    [LibraryImport(LibraryPath, EntryPoint = "destroy_dynamic_buffer")]
    public static partial int DynamicBuffer_Destroy(ref DynamicBuffer buffer);

    // UInt8
    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint8_try_encrypt_with_client_key_u8")]
    public static partial int UInt8_Encrypt(byte value, nint client_key, out nint fhe);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint8_decrypt")]
    public static partial int UInt8_Decrypt(nint fhe, nint client_key, out byte out_value);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint8_destroy")]
    public static partial int UInt8_Destroy(nint value);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint8_serialize")]
    public static partial int UInt8_Serialize(nint value, out DynamicBuffer out_buffer);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint8_deserialize")]
    public static partial int UInt8_Deserialize(DynamicBufferView buffer_view, out nint out_value);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint8_add")]
    public static partial int UInt8_Add(nint value1, nint value2, out nint out_value);

    // UInt32

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint32_try_encrypt_with_client_key_u32")]
    public static partial int UInt32_Encrypt(uint value, nint client_key, out nint fhe);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint32_decrypt")]
    public static partial int UInt32_Decrypt(nint fhe, nint client_key, out uint out_value);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint32_destroy")]
    public static partial int UInt32_Destroy(nint value);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint32_serialize")]
    public static partial int UInt32_Serialize(nint value, out DynamicBuffer out_buffer);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint32_deserialize")]
    public static partial int UInt32_Deserialize(DynamicBufferView buffer_view, out nint out_value);

    [LibraryImport(LibraryPath, EntryPoint = "fhe_uint32_add")]
    public static partial int UInt32_Add(nint value1, nint value2, out nint out_value);
}
