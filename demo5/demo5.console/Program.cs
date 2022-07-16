using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

internal class Program
{
    private static HttpClient client = new HttpClient();

    private static async Task Main(string[] args)
    {
        var len = await DownloadHtmlAsync("https://www.baidu.com", "web.txt");

        Console.WriteLine(len);
    }

    private static async Task<int> DownloadHtmlAsync(string url, string fileName)
    {
        using (client)
        {
            File.Delete(fileName);
            var content = await client.GetStringAsync(url);
            await File.WriteAllTextAsync(fileName, content);
            return content.Length;
        }
    }
}