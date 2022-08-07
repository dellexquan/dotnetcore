using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class PersonController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Person> GetAll()
    {
        return new List<Person> {
            new Person(1, "Dellex", 18),
            new Person(2, "Tom", 5),
        };
    }

    [HttpGet]
    public Person? GetById(long Id)
    {
        if (Id == 1)
        {
            return new Person(1, "Dellex", 18);
        }
        else if (Id == 2)
        {
            return new Person(2, "Tom", 5);
        }
        else
        {
            return null;
        }
    }

    [HttpPost]
    public string AddNew(Person p)
    {
        return "ok";
    }

    [HttpGet]
    public async Task<string> Add()
    {
        var s = await System.IO.File.ReadAllTextAsync("Dellex.txt");
        return s;
    }

    [HttpGet]
    public People Test2()
    {
        return new People
        {
            Id = 5,
            Name = "Dellex",
            ChildrenNames = new string[] { "aaa", "bbb" }
        };
    }

    [HttpGet("{i1}/{i2}")]
    public int Multiply(int i1, int i2)
    {
        return i1 * i2;
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string[]? ChildrenNames { get; set; }
    }
}