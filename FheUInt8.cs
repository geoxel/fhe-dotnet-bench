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
        int error = SafeNativeMethods.UInt8_Encrypt(clientKey.Handle, value, out nint out_value);
        if (error != 0)
            throw new FheException(error);
        return new FheUInt8(out_value);
    }

    public byte Decrypt(FheClientKey clientKey)
    {
        int error = SafeNativeMethods.UInt8_Decrypt(clientKey.Handle, Handle, out byte out_value);
        if (error != 0)
            throw new FheException(error);
        return out_value;
    }

    public byte[] Serialize()
    {
        int error = SafeNativeMethods.UInt8_Serialize(Handle, out SafeNativeMethods.DynamicBuffer buffer);
        if (error != 0)
            throw new FheException(error);
        return DynamicBufferToArray(buffer);
    }

    public static unsafe FheUInt8 Deserialize(byte[] data)
    {
        fixed (byte* ptr = data)
        {
            int error = SafeNativeMethods.UInt8_Deserialize(new nint(ptr), data.Length, out nint out_value);
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
