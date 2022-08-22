using System.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.filter.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DemoController : ControllerBase
{
    private readonly MyDbContext ctx;
    public DemoController(MyDbContext ctx)
    {
        this.ctx = ctx;
    }

    [HttpGet]
    public string Test1()
    {
        return System.IO.File.ReadAllText("ccc.txt");
    }

    [HttpGet]
    public string Test2()
    {
        return "Success";
    }

    [HttpPost]
    public string Test3()
    {
        //sqlite not support transaction
        // using (var ts = new TransactionScope())
        // {
        //     ctx.Books.Add(new Book
        //     {
        //         Name = "Book1",
        //         Price = 1.0d
        //     });
        //     ctx.Persons.Add(new Person
        //     {
        //         Name = "Dellex",
        //         Age = 18
        //     });
        //     ctx.SaveChanges();
        //     ts.Complete();
        // }

        ctx.Books.Add(new Book
        {
            Name = "Book1",
            Price = 1.0d
        });
        ctx.Persons.Add(new Person
        {
            Name = "Dellex",
            Age = 18
        });
        ctx.SaveChanges();

        return "Success";
    }
}
