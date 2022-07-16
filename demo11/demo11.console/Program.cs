using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
// using System.Delegate;

var t1 = File.ReadAllTextAsync("1.txt");
var t2 = File.ReadAllTextAsync("2.txt");
var t3 = File.ReadAllTextAsync("3.txt");

var results = await Task.WhenAll(t1, t2, t3);

foreach (var result in results)
{
    Console.WriteLine(result);
}

var total = (from result in results select result.Length).Sum();
Console.WriteLine(total);

static async IAsyncEnumerable<string> test()
{
    yield return "hello";
    yield return "yzk";
    yield return "youzack";
}

await foreach (var s in test())
{
    Console.WriteLine(s);
}

var f1 = () => { Console.WriteLine("sssss"); };
f1();

var f2 = (int i) => { return "mmmm" + i; };
Console.WriteLine(f2(1));