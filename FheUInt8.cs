using System;

namespace Fhe;

public sealed class FheUInt8 : FheHandle
{
    private FheUInt8(nint handle) : base(handle)
    {
    }

    public override void Dispose() =>
        SafeNativeMethods.UInt8_Destroy(GetHandleAndFlush());

    public static FheUInt8 Encrypt(FheClientKey clientKey, byte value)
    {
        int error = SafeNativeMethods.UInt8_Encrypt(value, clientKey.Handle, out nint out_value);
        if (error != 0)
            throw new FheException(error);
        return new FheUInt8(out_value);
    }

    public byte Decrypt(FheClientKey clientKey)
    {
        int error = SafeNativeMethods.UInt8_Decrypt(Handle, clientKey.Handle, out byte out_value);
        if (error != 0)
            throw new FheException(error);
        return out_value;
    }

    public byte[] Serialize()
    {
        int error = SafeNativeMethods.UInt8_Serialize(Handle, out SafeNativeMethods.DynamicBuffer buffer);
        if (error != 0)
            throw new FheException(error);
        return SafeNativeMethods.DynamicBuffer_ToArray(buffer);
    }

    public static unsafe FheUInt8 Deserialize(byte[] data)
    {
        fixed (byte* ptr = data)
        {
            var buffer_view = new SafeNativeMethods.DynamicBufferView
            {
                pointer = new nint(ptr),
                length = data.Length,
            };

            int error = SafeNativeMethods.UInt8_Deserialize(buffer_view, out nint out_value);
            if (error != 0)
                throw new FheException(error);
            return new FheUInt8(out_value);
        }
    }

    public static FheUInt8 operator +(FheUInt8 value1, FheUInt8 value2)
    {
        int error = SafeNativeMethods.UInt8_Add(value1.Handle, value2.Handle, out nint out_value);
        if (error != 0)
            throw new FheException(error);
        return new FheUInt8(out_value);
    }
}
