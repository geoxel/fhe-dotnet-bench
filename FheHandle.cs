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
}
