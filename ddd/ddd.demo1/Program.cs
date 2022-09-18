// var person = new Person
// {
//     Id = 1,
//     Name = "dellex"
// };
// System.Console.WriteLine("Person intialized.");
// using var ctx = new MyDbContext();
// ctx.Persons.Add(person);
// ctx.SaveChanges();
// System.Console.WriteLine("Person saved.");

using var ctx = new MyDbContext();
var person = ctx.Persons.First();
System.Console.WriteLine(person.Id);

