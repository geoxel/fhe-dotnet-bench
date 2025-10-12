using System;
using System.Threading;

namespace Fhe;

public static class Fhe
{
    public static void GenerateKeys(out FheClientKey clientKey, out FheServerKey serverKey)
    {
        int error = SafeNativeMethods.ConfigBuilderDefault(out nint config_builder);
        if (error != 0)
            throw new FheException(error);

        error = SafeNativeMethods.ConfigBuilderBuild(config_builder, out nint config);
        if (error != 0)
            throw new FheException(error);

        error = SafeNativeMethods.GenerateKeys(config, out nint client_key, out nint server_key);
        if (error != 0)
            throw new FheException(error);

        error = SafeNativeMethods.SetServerKey(server_key);
        if (error != 0)
        {
            SafeNativeMethods.ServerKey_Destroy(server_key);
            SafeNativeMethods.ClientKey_Destroy(client_key);
            throw new FheException(error);
        }

        clientKey = new FheClientKey(client_key);
        serverKey = new FheServerKey(server_key);
    }
}
