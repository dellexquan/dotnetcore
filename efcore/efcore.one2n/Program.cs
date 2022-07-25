using (var db = new EntityDbContext())
{
    var a1 = new Article
    {
        Title = "Title1",
        Content = "Content1",
        Comments = new List<Comment>
        {
            new Comment
            {
                Message = "Comment1.1"
            },
            new Comment
            {
                Message = "Comment1.2"
            }
        }
    };

    db.Articles.Add(a1);
    db.SaveChanges();
    System.Console.WriteLine("Complete!");
}