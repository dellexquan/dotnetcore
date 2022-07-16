using System.Threading.Tasks;
using System.Threading.Channels;
using System.Net.Http;
using System;

internal partial class Program
{
    private static HttpClient httpClient = new HttpClient();
    private static async Task Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        //cts.CancelAfter(200);
        Download2Async("https://www.baidu.com", 100, cts.Token);
        while (Console.ReadLine() != "q")
        {

        }
        cts.Cancel();
        Console.ReadLine();
    }

    private static async Task Download1Async(string url, int n)
    {
        using (httpClient)
        {
            for (var i = 0; i < n; i++)
            {
                var html = await httpClient.GetStringAsync(url);
                Console.WriteLine($"Downloaded {i + 1} time.");
            }
        }

    }

    private static async Task Download2Async(string url, int n, CancellationToken token)
    {
        using (httpClient)
        {
            for (var i = 0; i < n; i++)
            {
                var html = await httpClient.GetStringAsync(url);
                Console.WriteLine($"Downloaded {i + 1} time.");
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Canceled!!!");
                    break;
                }
            }
        }

    }

    private static async Task Download3Async(string url, int n, CancellationToken token)
    {
        using (httpClient)
        {
            for (var i = 0; i < n; i++)
            {
                var html = await httpClient.GetStringAsync(url);
                Console.WriteLine($"Downloaded {i + 1} time.");
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Canceled!!!");
                    break;
                }
            }
        }

    }
}