using System.Security.Cryptography;
using System.Text;

namespace BibliotecaUteco.Helpers;

public static class PasswordHasherHelper
{ 
    public static string Hash(this string password)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
    }

}