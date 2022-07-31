using System.Linq.Expressions;
using System.Text.Json;
using ExpressionTreeToString;
using static System.Linq.Expressions.Expression;

//PrintExpression1();
//PrintExpression2();

//BuildExpression();

//DynamicBuildExpression();
//PrintFactoryMehtods();

// Expression<Func<Book, bool>> expr = b => b.Price == 5;
// //Expression<Func<Book, bool>> expr = b => b.Title == "yzk";
// System.Console.WriteLine(expr.ToString("Factory methods", "C#"));

var books = new List<Book> {
    new Book {Id=1,Title="book1", Author="Dellex", Price=1.9d},
    new Book {Id=2, Title="book2", Author="Dellex", Price=5.2d}
};
var filterBooks = books.QueryBooks("Id", 1);
foreach (var book in filterBooks)
{
    PrintBookEntity(book);
}


void PrintBookEntity(Book book)
{
    System.Console.WriteLine(JsonSerializer.Serialize(book));
}

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

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public double Price { get; set; }
}

public static class BooksExtension
{
    public static IEnumerable<Book> QueryBooks(this IEnumerable<Book> books, string propertyName, object value)
    {
        // Expression<Func<Book, bool>> expr = b => b.Author == "Dellex";
        // return books.Where(expr.Compile());
        var b = Parameter(
            typeof(Book),
            "b"
        );
        var valueType = typeof(Book).GetProperty(propertyName)!.PropertyType;
        var val = System.Convert.ChangeType(value, valueType);
        Expression<Func<Book, bool>> expr;
        if (valueType.IsPrimitive)
        {
            expr = Lambda<Func<Book, bool>>(
                Equal(
                    MakeMemberAccess(b,
                        typeof(Book).GetProperty(propertyName)!
                    ),
                    Constant(val)
                ),
                b
            );
        }
        else
        {
            expr = Lambda<Func<Book, bool>>(
                MakeBinary(ExpressionType.Equal,
                    MakeMemberAccess(b,
                        typeof(Book).GetProperty(propertyName)!
                    ),
                    Constant(val), false,
                    typeof(string).GetMethod("op_Equality")
                ),
                b
            );
        }

        return books.Where(expr.Compile());
    }
}
