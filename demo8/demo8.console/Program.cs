using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System;

internal class Program
{
    //private const string fileName = "t.txt";

    private static async Task Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");
        // PrintThreadId();

        // var result = await CalcAsync(2);

        // Console.WriteLine($"Calc Result: {result}");
        // PrintThreadId();

        // var s = await ReadAsync(0);
        // Console.WriteLine(s);
        // s = await ReadAsync(1);
        // Console.WriteLine(s);

        Console.WriteLine("Hello, World!");
        PrintThreadId();

        var result = await Calc2Async(2);

        Console.WriteLine($"Calc Result: {result}");
        PrintThreadId();
    }

    private static void PrintThreadId()
    {
        Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");
    }

    private static async Task<decimal> CalcAsync(int n)
    {
        // Console.WriteLine($"CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        // var result = 1m;

        // var rand = new Random();

        // for (var i = 0; i < n * n; i++)
        // {
        //     result += (decimal)rand.NextDouble();
        // }

        // return result;

        return await Task.Run(() =>
        {
            Console.WriteLine($"CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            var result = 1m;

            var rand = new Random();

            for (var i = 0; i < n * n; i++)
            {
                result += (decimal)rand.NextDouble();
            }

            return result;

        });
    }

    private static Task<decimal> Calc2Async(int n)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            var result = 1m;

            var rand = new Random();

            for (var i = 0; i < n * n; i++)
            {
                result += (decimal)rand.NextDouble();
            }

            return Task.FromResult(result);

        });
    }

    private static Task<string> ReadAsync(int n)
    {
        if (n == 0)
        {
            return File.ReadAllTextAsync("t.txt");
        }
        else if (n == 1)
        {
            return File.ReadAllTextAsync("1.txt");
        }

        throw new ArgumentException();

    }
}