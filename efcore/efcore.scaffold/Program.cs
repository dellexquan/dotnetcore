using efcore.scaffold;

using (var db = new entityContext())
{
    var books = db.Books!.Select(b => new { Id = b.Id, Title = b.Title, AuthorName = b.AuthorName, PubTime = b.PubTime, Price = b.Price }).OrderBy(b => b.Price);
    foreach (var book in books)
    {
        System.Console.WriteLine(book);
    }
    System.Console.WriteLine("===============");
    var perons = db.Persons!.Select(p => new { Id = p.Id, Name = p.Name, Age = p.Age, BirthPlace = p.BirthPlace });
    foreach (var person in perons)
    {
        System.Console.WriteLine(person);
    }
}
