using System;
using System.Diagnostics;
using System.Globalization;

namespace Fhe;

public static class Program
{
    private static TimeSpan Benchmark(Action<object[], int> func, object[] prms, int iterations = 1000)
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
            func(prms, i);
        return stopwatch.Elapsed / iterations;
    }

    private static void GoTesting8(object[] prms, int index)
    {
        var clientKey = (FheClientKey)prms[0];

        byte a = (byte)index;
        using var enc_a = FheUInt8.Encrypt(clientKey, a);
        byte[] serialized_enc_a = enc_a.Serialize();
        using var enc_a2 = FheUInt8.Deserialize(serialized_enc_a);
        byte a2 = enc_a2.Decrypt(clientKey);
        if (a2 != a)
            throw new InvalidDataException();
    }

    private static void GoTesting32(object[] prms, int index)
    {
        var clientKey = (FheClientKey)prms[0];

        uint a = (uint)index;
        using var enc_a = FheUInt32.Encrypt(clientKey, a);
        byte[] serialized_enc_a = enc_a.Serialize();
        using var enc_a2 = FheUInt32.Deserialize(serialized_enc_a);
        uint a2 = enc_a2.Decrypt(clientKey);
        if (a2 != a)
            throw new InvalidDataException();
    }

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        Console.WriteLine("Generate keys...");

        Fhe.GenerateKeys(out FheClientKey clientKey, out FheServerKey serverKey);

        Console.WriteLine("Encrypting...");

        using var a = FheUInt32.Encrypt(clientKey, 42);
        using var b = FheUInt32.Encrypt(clientKey, 18);

        byte[] serialized_a = a.Serialize();
        FheUInt32 deserialized_a = FheUInt32.Deserialize(serialized_a);
        Console.WriteLine($"Serialized length: {serialized_a.Length}, deserialized value: {deserialized_a.Decrypt(clientKey)}");

        Console.WriteLine("Adding...");

        using FheUInt32 c = a + b;

        Console.WriteLine("Decrypting...");

        Console.WriteLine("a + b = " + c.Decrypt(clientKey));

        Console.WriteLine("Benchmarking...");

        TimeSpan duration;

        duration = Benchmark(GoTesting8, new object[] { clientKey }, iterations: 500 * 10);
        Console.WriteLine($"FheUInt8 : {duration.TotalMilliseconds:n2} ms");

        duration = Benchmark(GoTesting32, new object[] { clientKey }, iterations: 500 * 5);
        Console.WriteLine($"FheUInt32: {duration.TotalMilliseconds:n2} ms");

        Console.WriteLine("All done");

        clientKey.Dispose();
        serverKey.Dispose();
    }
}
