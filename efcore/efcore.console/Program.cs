// var newBook = new Book
// {
//     // Id = 1,
//     Title = "Dellex Book II",
//     PubTime = DateTime.Now,
//     AuthorName = "Dellex Quan",
//     Price = 0.89d
// };

// var person = new Person
// {
//     Id = 1,
//     Name = "Dellex Quan",
//     Age = 38
// };

using Microsoft.EntityFrameworkCore;

using (var db = new EntityDbContext())
{
    //db.Books!.Add(newBook);
    //db.Persons!.Add(person);
    //await db.SaveChangesAsync();
    // var p = db.Persons!.First(p => p.Id == 1);
    // p.BirthPlace = "Shanghai";
    // db.SaveChanges();
    // var b = db.Books!.First(b => b.Id == 1);
    // b.AuthorName = "Dellex Quan";
    // db.SaveChanges();

    // var newBooks = new List<Book>();
    // newBooks.Add(new Book
    // {
    //     Title = "Program C",
    //     PubTime = DateTime.Now,
    //     AuthorName = "yzk",
    //     Price = 3.00d
    // });
    // newBooks.Add(new Book
    // {
    //     Title = "Program C",
    //     PubTime = DateTime.Now,
    //     AuthorName = "yzk",
    //     Price = 3.00d
    // });
    // newBooks.Add(new Book
    // {
    //     Title = "Program C from Zero",
    //     PubTime = new DateTime(2019, 3, 1),
    //     AuthorName = "yzk",
    //     Price = 59.80d
    // });
    // newBooks.Add(new Book
    // {
    //     Title = "Algrithm 4.0",
    //     PubTime = new DateTime(2012, 10, 1),
    //     AuthorName = "Robert",
    //     Price = 99.00d
    // });
    // newBooks.Add(new Book
    // {
    //     Title = "Beautry of Math",
    //     PubTime = new DateTime(2020, 5, 1),
    //     AuthorName = "Wu Jun",
    //     Price = 69.00d
    // });
    // newBooks.Add(new Book
    // {
    //     Title = "SQL",
    //     PubTime = new DateTime(2008, 9, 1),
    //     AuthorName = "yzk",
    //     Price = 52.00d
    // });
    // newBooks.Add(new Book
    // {
    //     Title = "Light of Civil",
    //     PubTime = new DateTime(2017, 3, 1),
    //     AuthorName = "Wu Jun",
    //     Price = 246.00d
    // });
    // db.Books!.AddRange(newBooks);
    // await db.SaveChangesAsync();


    var books = db.Books!.Select(b => new { Id = b.Id, Title = b.Title, AuthorName = b.AuthorName, PubTime = b.PubTime, Price = b.Price }).OrderBy(b => b.Price);
    //System.Console.WriteLine(books.ToQueryString());
    foreach (var book in books)
    {
        System.Console.WriteLine(book);
    }
    System.Console.WriteLine("======== =======");
    var perons = db.Persons!.Select(p => new { Id = p.Id, Name = p.Name, Age = p.Age, BirthPlace = p.BirthPlace });
    foreach (var person in perons)
    {
        System.Console.WriteLine(person);
    }

    System.Console.WriteLine("===============");
    var query = db.Books!.Where(b => b.Price > 80).Select(b => new { Id = b.Id, Title = b.Title, AuthorName = b.AuthorName, PubTime = b.PubTime, Price = b.Price });
    foreach (var book in query)
    {
        System.Console.WriteLine(book);
    }

    System.Console.WriteLine("===============");
    query = db.Books!.Where(b => b.Title == "SQL").Select(b => new { Id = b.Id, Title = b.Title, AuthorName = b.AuthorName, PubTime = b.PubTime, Price = b.Price });
    System.Console.WriteLine(query.Single());

    System.Console.WriteLine("===============");
    var groups = from b in db.Books
                 group b by b.AuthorName into g
                 select new { AuthorName = g.Key, BooksCount = g.Count(), MaxPrice = g.Max(b => b.Price) };
    foreach (var group in groups)
    {
        System.Console.WriteLine(group);
    }
    // System.Console.WriteLine("================");
    // var query2 = db.Books!.Where(b => b.Price > 10);
    // foreach (var book in query2)
    // {
    //     book.Price += 1.0d;
    // }
    // await db.SaveChangesAsync();
}