using System.Linq.Expressions;
using ExpressionTreeToString;

//PrintExpression1();
//PrintExpression2();

//BuildExpression();

//DynamicBuildExpression();
PrintFactoryMehtods();


void PrintExpression1()
{
    Expression<Func<string, bool>> expr = s => s.Count() > 5;
    Console.WriteLine(expr.ToString("Object notation", "C#"));
}

void PrintExpression2()
{
    Expression<Func<int, int, int>> expr2 = (a, b) => a + b;
    System.Console.WriteLine(expr2.ToString("Object notation", "C#"));
}

void PrintFactoryMehtods()
{
    Expression<Func<string, bool>> expr = s => s.Count() > 5;
    Console.WriteLine(expr.ToString("Factory methods", "C#"));
}

void BuildExpression()
{
    var paramA = Expression.Parameter(typeof(int), "a");
    var paramB = Expression.Parameter(typeof(int), "b");
    var add = Expression.MakeBinary(ExpressionType.Add, paramA, paramB);
    var expr3 = Expression.Lambda(add, true, new List<ParameterExpression> { paramA, paramB });
    System.Console.WriteLine(expr3.ToString("Object notation", "C#"));
    var result = expr3.Compile().DynamicInvoke(1, 2);
    System.Console.WriteLine(result);
}

void DynamicBuildExpression()
{
    System.Console.WriteLine("Please choose the mode: 1. add 2. multiply");
    var mode = int.Parse(Console.ReadLine()!);
    var paramA = Expression.Parameter(typeof(int), "a");
    var paramB = Expression.Parameter(typeof(int), "b");
    var type = mode == 1 ? ExpressionType.Add : ExpressionType.Multiply;
    var add = Expression.MakeBinary(type, paramA, paramB);
    var expr3 = Expression.Lambda(add, true, new List<ParameterExpression> { paramA, paramB });
    System.Console.WriteLine(expr3.ToString("Object notation", "C#"));
    var result = expr3.Compile().DynamicInvoke(1, 2);
    System.Console.WriteLine(result);
}