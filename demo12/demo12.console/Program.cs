using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        var nums = new int[] { 3, 5, 3334, 23, 4, 7, 34, 67 };
        //var result = nums.Where(i => i > 10);
        //var result = MyWhere1(nums, i => i > 10);
        var result = nums.AsEnumerable().MyWhere2(i => i > 10);
        foreach (var num in result)
        {
            Console.WriteLine(num);
        }

        // Console.WriteLine("================");



    }
    static IEnumerable<int> MyWhere1(IEnumerable<int> items, Func<int, bool> f)
    {
        var result = new List<int>();
        foreach (var i in items)
        {
            if (f(i))
                result.Add(i);
        }
        return result;
    }
}


public static class MyExtensions
{
    public static IEnumerable<int> MyWhere2(this IEnumerable<int> items, Func<int, bool> f)
    {
        foreach (var i in items)
        {
            if (f(i))
                yield return i;
        }
    }
}