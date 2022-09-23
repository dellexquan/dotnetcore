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

// using var ctx = new MyDbContext();
// var user = ctx.Users.Where(u => u.UserName == "yzk").First();
// user.Gender = Gender.Male;
// ctx.SaveChanges();
// System.Console.WriteLine("Save user completed!");

// using var ctx = new MyDbContext();
// var shop = new Shop { Name = "Dellex Shop", Location = new Geo(3, 5) };
// ctx.Shops.Add(shop);
// ctx.SaveChanges();
// System.Console.WriteLine("Shop save completed!");

// using var ctx = new MyDbContext();
// foreach (var shop in ctx.Shops)
// {
//     System.Console.WriteLine(shop.Id);
//     System.Console.WriteLine(shop.Name);
//     System.Console.WriteLine(shop.Location);
// }

using var ctx = new MyDbContext();
// var blog = new Blog
// {
//     Title = new MultiLangString("你好", "Hello"),
//     Body = new MultiLangString("fffff", "xxxxxx")
// };
// var blog = new Blog
// {
//     Title = new MultiLangString("再见", "Bye"),
//     Body = new MultiLangString("abc", "efg")
// };
// ctx.Blogs.Add(blog);
// ctx.SaveChanges();
// foreach (var blog in ctx.Blogs)
// {
//     System.Console.WriteLine(blog);
// }
var blog = ctx.Blogs.First(b => b.Title.Chinese == "你好" && b.Title.English == "Hello");
System.Console.WriteLine(blog);
