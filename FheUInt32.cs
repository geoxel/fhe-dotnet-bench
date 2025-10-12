using System;

namespace Fhe;

public sealed class FheUInt32 : FheHandle
{
    private FheUInt32(nint handle) : base(handle)
    {
    }

    public override void Dispose() =>
        SafeNativeMethods.UInt32_Destroy(GetHandleAndFlush());

    public static FheUInt32 Encrypt(FheClientKey clientKey, uint value)
    {
        int error = SafeNativeMethods.UInt32_Encrypt(value, clientKey.Handle, out nint out_value);
        if (error != 0)
            throw new FheException(error);
        return new FheUInt32(out_value);
    }

    public uint Decrypt(FheClientKey clientKey)
    {
        int error = SafeNativeMethods.UInt32_Decrypt(Handle, clientKey.Handle, out uint out_value);
        if (error != 0)
            throw new FheException(error);
        return out_value;
    }

    public byte[] Serialize()
    {
        int error = SafeNativeMethods.UInt32_Serialize(Handle, out SafeNativeMethods.DynamicBuffer buffer);
        if (error != 0)
            throw new FheException(error);
        return SafeNativeMethods.DynamicBuffer_ToArray(buffer);
    }

    public static unsafe FheUInt32 Deserialize(byte[] data)
    {
        fixed (byte* ptr = data)
        {
            var buffer_view = new SafeNativeMethods.DynamicBufferView
            {
                pointer = new nint(ptr),
                length = data.Length,
            };

            int error = SafeNativeMethods.UInt32_Deserialize(buffer_view, out nint out_value);
            if (error != 0)
                throw new FheException(error);
            return new FheUInt32(out_value);
        }
    }

    public static FheUInt32 operator +(FheUInt32 value1, FheUInt32 value2)
    {
        int error = SafeNativeMethods.UInt32_Add(value1.Handle, value2.Handle, out nint out_value);
        if (error != 0)
            throw new FheException(error);
        return new FheUInt32(out_value);
    }
}
