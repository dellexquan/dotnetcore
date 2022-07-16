using System.Collections.Generic;
using System.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var list = new List<Employee>();
list.Add(new Employee { Id = 1, Name = "Jerry", Age = 28, Gender = true, Salary = 5000 });
list.Add(new Employee { Id = 2, Name = "Jim", Age = 33, Gender = true, Salary = 3000 });
list.Add(new Employee { Id = 3, Name = "Lily", Age = 35, Gender = false, Salary = 9000 });
list.Add(new Employee { Id = 4, Name = "Lucy", Age = 16, Gender = false, Salary = 2000 });
list.Add(new Employee { Id = 5, Name = "Kimi", Age = 25, Gender = true, Salary = 1000 });
list.Add(new Employee { Id = 6, Name = "Nancy", Age = 35, Gender = false, Salary = 8000 });
list.Add(new Employee { Id = 7, Name = "Zack", Age = 35, Gender = true, Salary = 8500 });
list.Add(new Employee { Id = 8, Name = "Jack", Age = 33, Gender = true, Salary = 8000 });

//var query = list.Where(e => e.Age >= 30);
var query = from e in list orderby e.Age, e.Salary descending select new { Id = e.Id, Name = e.Name };

foreach (var emp in query)
{
    Console.WriteLine(emp);
}

Console.WriteLine(list.Count(e => e.Gender == true));

record Employee
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public bool Gender { get; set; }
    public int Salary { get; set; }
}


