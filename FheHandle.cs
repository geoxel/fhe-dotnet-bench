using System;
using System.Runtime.InteropServices;

namespace Fhe;

public abstract class FheHandle : IDisposable
{
    private nint _handle;

    public nint Handle => _handle;

    protected FheHandle(nint handle)
    {
        _handle = handle;
    }

    public abstract void Dispose();

    protected nint GetHandleAndFlush() =>
        Interlocked.CompareExchange(ref _handle, value: IntPtr.Zero, comparand: _handle);

    internal static byte[] DynamicBufferToArray(SafeNativeMethods.DynamicBuffer buffer)
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
            SafeNativeMethods.DynamicBuffer_Destroy(ref buffer);
        }
    }
}
