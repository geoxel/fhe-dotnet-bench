using System;

namespace Fhe;

public sealed class FheServerKey : FheHandle
{
    internal FheServerKey(nint handle) : base(handle)
    {
    }

    public override void Dispose() =>
        SafeNativeMethods.ServerKey_Destroy(GetHandleAndFlush());
}
