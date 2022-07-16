using System.IO;
using System.Text;

var fileName = "1.txt";
File.Delete(fileName);
var sb = new StringBuilder();
for (var i = 0; i < 10000; i++)
{
    sb.Append("hello ");
}
await File.WriteAllTextAsync(fileName, sb.ToString());
var s = await File.ReadAllTextAsync(fileName);
Console.WriteLine(s);