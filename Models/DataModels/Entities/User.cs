using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataModel.Entities;

public class User : EntityBase
{
    public string Login { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
    private static byte[] GetHash(string inputString)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}
