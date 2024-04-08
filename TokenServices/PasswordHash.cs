using System;
using System.Security.Cryptography;
using System.Text;

namespace Parking.TokenServices;

public class PasswordHash 
{
    public string HashPassword(string password)
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Create().GetBytes(salt);

        var pb = new Rfc2898DeriveBytes(password, salt, 10000,  HashAlgorithmName.SHA256);

        byte[] hash = pb.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pb = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        byte[] hash = pb.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
                return false;
        }
        return true;
    }
}
