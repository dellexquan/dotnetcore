using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using (var db = new EntityDbContext())
{
    //Insert(db);
    //QueryArticle(db);
    //QueryComment(db);
    QueryCommentWithArticleId(db);
}

void QueryCommentWithArticleId(EntityDbContext db)
{
    var comment = db.Comments.FirstOrDefault();
    PrintCommentWithArticleId(comment!);
}

void PrintCommentWithArticleId(Comment comment)
{
    var c = new
    {
        comment.Id,
        comment.Message,
        comment.ArticleId
    };
    System.Console.WriteLine(JsonSerializer.Serialize(c));
}

void QueryComment(EntityDbContext db)
{
    var comment = db.Comments.Include(c => c.Article).FirstOrDefault();
    PrintComment(comment!);
}

void PrintComment(Comment comment)
{
    PrintCommentEntity(comment);
    if (comment.Article != null)
        PrintArticleEntity(comment.Article);
}

void PrintArticle(Article article)
{
    PrintArticleEntity(article);
    foreach (var comment in article.Comments)
    {
        PrintCommentEntity(comment);
    }
}

void PrintCommentEntity(Comment comment)
{
    var c = new
    {
        comment.Id,
        comment.Message
    };
    System.Console.WriteLine(JsonSerializer.Serialize(c));
}

void PrintArticleEntity(Article article)
{
    var a = new
    {
        article.Id,
        article.Title,
        article.Content
    };
    System.Console.WriteLine(JsonSerializer.Serialize(a));
}

void QueryArticle(EntityDbContext db)
{
    var article = db.Articles.Include(a => a.Comments).FirstOrDefault();
    PrintArticle(article!);
}

void Insert(EntityDbContext db)
{
    var a1 = new Article
    {
        Title = "Title2",
        Content = "Content2",
        Comments = new List<Comment>
        {
            new Comment
            {
                Message = "Comment2.1"
            },
            new Comment
            {
                Message = "Comment2.2"
            }
        }
    };

    db.Articles.Add(a1);
    db.SaveChanges();
}