// See https://aka.ms/new-console-template for more information

System.Console.WriteLine("Please input percent(0-100):");
var percent = Convert.ToInt32(Console.ReadLine());
System.Console.WriteLine("Please input years:");
var yearNum = Convert.ToInt32(Console.ReadLine());

System.Console.WriteLine($"Percent: {percent}");
System.Console.WriteLine($"Years: {yearNum}");

var percents = new List<double>();
for (var i = 1; i <= yearNum; i++)
{
    var p = Convert.ToDouble((percent - 3)) / i;
    percents.Add(p);
    System.Console.WriteLine($"Year {i}: {p}");
}
var avg = Math.Round(percents.Average(), 2);
System.Console.WriteLine($"Avg Percent: {avg}");
