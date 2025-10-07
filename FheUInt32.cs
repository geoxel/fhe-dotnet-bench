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
        int error = SafeNativeMethods.UInt32_Encrypt(clientKey.Handle, value, out nint out_value);
        if (error != 0)
            throw new FheException(error);
        return new FheUInt32(out_value);
    }

    public uint Decrypt(FheClientKey clientKey)
    {
        int error = SafeNativeMethods.UInt32_Decrypt(clientKey.Handle, Handle, out uint out_value);
        if (error != 0)
            throw new FheException(error);
        return out_value;
    }

    public byte[] Serialize()
    {
        int error = SafeNativeMethods.UInt32_Serialize(Handle, out SafeNativeMethods.DynamicBuffer buffer);
        if (error != 0)
            throw new FheException(error);
        return DynamicBufferToArray(buffer);
    }

    public static unsafe FheUInt32 Deserialize(byte[] data)
    {
        fixed (byte* ptr = data)
        {
            int error = SafeNativeMethods.UInt32_Deserialize(new nint(ptr), data.Length, out nint out_value);
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
