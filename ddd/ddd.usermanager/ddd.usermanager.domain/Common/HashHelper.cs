using System.Security.Cryptography;
using System.Text;

namespace ddd.usermanager.domain;
public static class HashHelper
{
    public static string ComputeMd5Hash(string input)
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