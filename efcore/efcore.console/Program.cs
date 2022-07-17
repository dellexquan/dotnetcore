// var book = new Book
// {
//     Id = 1,
//     Title = "Dellex Book",
//     PubTime = DateTime.Now,
//     Price = 0.99d
// };

// var person = new Person
// {
//     Id = 1,
//     Name = "Dellex Quan",
//     Age = 38
// };

using (var db = new EntityDbContext())
{
    //db.Books!.Add(book);
    //db.Persons!.Add(person);
    //db.SaveChanges();
    // var p = db.Persons!.First(p => p.Id == 1);
    // p.BirthPlace = "Shanghai";
    // db.SaveChanges();
    // var b = db.Books!.First(b => b.Id == 1);
    // b.AuthorName = "Dellex Quan";
    // db.SaveChanges();

    var books = db.Books!.Select(b => new { Id = b.Id, Title = b.Title, AuthorName = b.AuthorName, PubTime = b.PubTime, Price = b.Price });
    foreach (var book in books)
    {
        System.Console.WriteLine(book);
    }

    var perons = db.Persons!.Select(p => new { Id = p.Id, Name = p.Name, Age = p.Age, BirthPlace = p.BirthPlace });
    foreach (var person in perons)
    {
        System.Console.WriteLine(person);
    }
}