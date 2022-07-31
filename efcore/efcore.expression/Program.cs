using System.Linq.Expressions;
using ExpressionTreeToString;

// Expression<Func<string, bool>> expr = s => s.Count() > 5;
// Console.WriteLine(expr.ToString("Object notation", "C#"));

Expression<Func<int, int, int>> expr2 = (a, b) => a + b;
System.Console.WriteLine(expr2.ToString("Object notation", "C#"));