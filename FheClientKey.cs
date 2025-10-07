using System;

namespace Fhe;

public sealed class FheClientKey : FheHandle
{
    public FheClientKey(nint handle) : base(handle)
    {
    }

    public override void Dispose() =>
        SafeNativeMethods.ClientKey_Destroy(GetHandleAndFlush());
}
