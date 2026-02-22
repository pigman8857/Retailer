using Infras.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Infras.Identity;

public class PasswordHashingService : IPasswordService
{
  public PasswordHashingService()
  {

  }

  public string HashPassword(string password, byte[] salt)
  {
    //https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-10.0

    Console.Write("PasswordHashingService >>> Enter a password: ", password);

    Console.WriteLine($">>>>>> Salt: {Convert.ToBase64String(salt)}");

    // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password!,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8));

    Console.WriteLine($"Hashed: {hashed}");
    return hashed;
  }

  public byte[] GetSalt()
  {
    // divide by 8 to convert bits to bytes
    return RandomNumberGenerator.GetBytes(128 / 8);
  }
}
