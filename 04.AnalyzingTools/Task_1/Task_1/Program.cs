using System.Diagnostics;
using System.Security.Cryptography;

namespace Task_1;

internal class Program
{
    static void Main(string[] args)
    {
        var password = "My$tr0ngPa$$w0rd";
        var rng = new RNGCryptoServiceProvider();
        var salt = new byte[32];
        rng.GetNonZeroBytes(salt);

        var stopWatch = new Stopwatch();

        stopWatch.Start();
        var generatedHash = GeneratePasswordHash(password, salt);
        stopWatch.Stop();
        Console.WriteLine($"GeneratePasswordHash: {stopWatch.Elapsed.Milliseconds}");

        stopWatch.Restart();
        var generatedHashImproved = GeneratePasswordHashImproved(password, salt);
        stopWatch.Stop();
        Console.WriteLine($"GeneratePasswordHashImproved: {stopWatch.Elapsed.Milliseconds}");

    }

    static string GeneratePasswordHash(string passwordText, byte[] salt)
    {
        var iterate = 10000;
        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var passwordHash = Convert.ToBase64String(hashBytes);

        return passwordHash;
    }

    static string GeneratePasswordHashImproved(string passwordText, byte[] salt)
    {
        const int saltLength = 16;
        const int hashLength = 20;
        const int totalLength = saltLength + hashLength;
        var iterate = 10000;

        byte[] hashBytes = new byte[totalLength];

        Buffer.BlockCopy(salt, 0, hashBytes, 0, saltLength);

        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
        {
            var hash = pbkdf2.GetBytes(hashLength);
            Buffer.BlockCopy(hash, 0, hashBytes, saltLength, hashLength);
        }

        return Convert.ToBase64String(hashBytes);

    }
}
