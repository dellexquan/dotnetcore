using System.Threading;
using System.Text;
using System.IO;

Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
var sb = new StringBuilder();
sb.Append("x"); 
// for (var i = 0; i < 1; i++)
// {
//     sb.Append("XXXXXXXXXXXXXXXX");
// }
File.Delete("1.txt");
await File.WriteAllTextAsync("1.txt", sb.ToString());
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
