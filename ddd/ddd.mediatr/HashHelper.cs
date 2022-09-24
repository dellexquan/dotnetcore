using System.Security.Cryptography;
using System.Text;

public static class HashHelper
{
    public static string GetMD5Hash(string input)
    {
        using var md5Hash = MD5.Create();
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }
}