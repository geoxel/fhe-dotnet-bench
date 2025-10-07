using System;
using System.Threading;

namespace Fhe;

public static class Fhe
{
    public static void GenerateKeys(out FheClientKey clientKey, out FheServerKey serverKey)
    {
        int error = SafeNativeMethods.GenerateKeys(out nint client_key, out nint server_key);
        if (error != 0)
            throw new FheException(error);
        clientKey = new FheClientKey(client_key);
        serverKey = new FheServerKey(server_key);
    }
}
