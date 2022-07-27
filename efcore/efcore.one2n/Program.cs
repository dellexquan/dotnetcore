using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using (var db = new EntityDbContext())
{
    //InsertArticle(db);
    QueryArticle(db);
    //QueryComment(db);
    //QueryCommentWithArticleId(db);
    //InsertLeave(db);
    //QueryLeave(db);
}

void QueryLeave(EntityDbContext db)
{
    var leave = db.Leaves.Include(l => l.Approver).Include(l => l.Requester).FirstOrDefault();
    PrintLeave(leave!);
}

void PrintLeave(Leave leave)
{
    PrintLeaveEntity(leave);
    PrintUserEntity(leave.Requester);
    if (leave.Approver != null)
        PrintUserEntity(leave.Approver);
}

void PrintUserEntity(User user)
{
    System.Console.WriteLine(JsonSerializer.Serialize(user));
}

void PrintLeaveEntity(Leave leave)
{
    var l = new
    {
        leave.Id,
        leave.ApproverId,
        leave.RequesterId,
        leave.Remarks
    };
    System.Console.WriteLine(JsonSerializer.Serialize(l));
}

void InsertLeave(EntityDbContext db)
{
    var leave = new Leave
    {
        Requester = new User { Name = "Dellex Quan" },
        Approver = new User { Name = "YZK" },
        Remarks = "Approve!"
    };
    db.Leaves.Add(leave);
    db.SaveChanges();
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

void InsertArticle(EntityDbContext db)
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