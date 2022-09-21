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

// using var ctx = new MyDbContext();
// var person = ctx.Persons.First();
// System.Console.WriteLine(person.Id);

// using var ctx = new MyDbContext();
// var user = new User("yzk", Gender.Male);
// user.ChangePassword("123456");
// ctx.Users.Add(user);
// ctx.SaveChanges();
// System.Console.WriteLine("Save user completed!");

using var ctx = new MyDbContext();
var user = ctx.Users.Where(u => u.UserName == "yzk").First();
user.Gender = Gender.Male;
ctx.SaveChanges();
System.Console.WriteLine("Save user completed!");