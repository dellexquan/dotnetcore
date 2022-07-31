using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using (var db = new EntityDbContext())
{
    //InsertArticle(db);
    //QueryArticle(db);
    //QueryComment(db);
    //QueryCommentWithArticleId(db);
    //InsertLeave(db);
    //QueryLeave(db);
    //InsertOrg(db);
    //QueryOrg(db);
    //InsertOrder(db);
    //QueryOrder(db);
    //QueryDelivery(db);
    //InsertStudent(db);
    //Qu eryStudent(db);
    //InsertTeacher(db);
    //QueryTeacher(db);
    //ComplexQueryArticle(db);
    //ComplexQueryOrder(db);
    //await InsertArticleWithRawSql(db);
    //QueryArticlesWithRawSql(db);
    //await QueryArticlesWithADO(db);
    UpdateArticle(db);
}

void UpdateArticle(EntityDbContext db)
{
    var article = db.Articles.Find(3L);
    article!.Title = "Title6";
    article!.Content = "Content6";
    db.SaveChanges();
}

async Task QueryArticlesWithADO(EntityDbContext db)
{
    var conn = db.Database.GetDbConnection();
    if (conn.State != System.Data.ConnectionState.Open)
    {
        conn.Open();
    }
    using (var cmd = conn.CreateCommand())
    {
        cmd.CommandText = @"select a.Id, a.Title, a.Content
        , case when max(c.Id) > 0 then count(*) else 0 end as CommentNum 
        from Articles a 
        left join Comments c on c.ArticleId = a.Id
        group by a.Id, a.Title, a.Content;
        ";
        using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var article = new
                {
                    Id = reader.GetInt64(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    CommentNum = reader.GetInt32(3)
                };
                System.Console.WriteLine(JsonSerializer.Serialize(article));
            }
        }
    }
}

void QueryArticlesWithRawSql(EntityDbContext db)
{
    var articles = QueryArticleWithRawSql(db);
    foreach (var article in articles)
    {
        PrintArticleEntity(article);
    }
}

IQueryable<Article> QueryArticleWithRawSql(EntityDbContext db)
{
    var minId = 3;
    return db.Articles.FromSqlInterpolated(@$"select * from Articles where ID > {minId}");
}

async Task InsertArticleWithRawSql(EntityDbContext db)
{
    var a1 = new Article
    {
        Title = "Title5",
        Content = "Content5"
    };
    //var sql = $"insert into Articles (Title, Content) values (\"{a1.Title}\",\"{a1.Content}\")";await db.Database.ExecuteSqlRawAsync(sql);
    //var sql = $"insert into Articles (Title, Content) values (\"{a1.Title}\",\"{a1.Content}\")";
    await db.Database.ExecuteSqlInterpolatedAsync(@$"insert into Articles (Title, Content) values ({a1.Title},{a1.Content})");
}

void ComplexQueryOrder(EntityDbContext db)
{
    var orders = db.Orders.Where(o => o.Delivery.CompanyName == "Dellex Company");
    foreach (var order in orders)
    {
        PrintOrderEntity(order);
    }
}

void InsertStudent(EntityDbContext db)
{
    var student = new Student
    {
        Name = "Student001",
        Teachers = new List<Teacher>
        {
            new Teacher { Name = "Teacher001" },
            new Teacher { Name = "Teacher002" }
        }
    };
    db.Students.Add(student);
    db.SaveChanges();
}

void InsertTeacher(EntityDbContext db)
{
    var teacher = db.Teachers.FirstOrDefault();
    teacher!.Students.Add(new Student
    {
        Name = "Student002"
    });
    db.SaveChanges();
}

void QueryTeacher(EntityDbContext db)
{
    var teacher = db.Teachers.Include(t => t.Students).FirstOrDefault();
    PrintTeacherEntity(teacher!);
    foreach (var student in teacher!.Students)
    {
        PrintStudentEntity(student);
    }
}

void QueryStudent(EntityDbContext db)
{
    var student = db.Students.Include(s => s.Teachers).FirstOrDefault();
    PrintStudentEntity(student!);
    foreach (var teacher in student!.Teachers)
    {
        PrintTeacherEntity(teacher);
    }
}

void PrintTeacherEntity(Teacher teacher)
{
    var t = new
    {
        teacher.Id,
        teacher.Name
    };
    System.Console.WriteLine(JsonSerializer.Serialize(t));
}

void PrintStudentEntity(Student student)
{
    var s = new
    {
        student.Id,
        student.Name
    };
    System.Console.WriteLine(JsonSerializer.Serialize(s));
}

void QueryDelivery(EntityDbContext db)
{
    var delivery = db.Deliveries.Include(d => d.Order).FirstOrDefault();
    PrintDeliveryEntity(delivery!);
    PrintOrderEntity(delivery!.Order);
}

void QueryOrder(EntityDbContext db)
{
    var order = db.Orders.Include(o => o.Delivery).FirstOrDefault();
    PrintOrderEntity(order!);
    if (order!.Delivery != null)
        PrintDeliveryEntity(order.Delivery);
}

void PrintDeliveryEntity(Delivery delivery)
{
    var d = new
    {
        delivery.Id,
        delivery.CompanyName,
        delivery.Number,
        delivery.OrderId
    };
    System.Console.WriteLine(JsonSerializer.Serialize(d));
}

void PrintOrderEntity(Order order)
{
    var o = new
    {
        order.Id,
        order.Name,
        order.Address
    };
    System.Console.WriteLine(JsonSerializer.Serialize(o));
}

void InsertOrder(EntityDbContext db)
{
    var order = new Order
    {
        Name = "Dellex Product2",
        Address = "Dellex Street No 2",
        Delivery = new Delivery
        {
            CompanyName = "Dellex Company2",
            Number = "Dellex002"
        }
    };
    db.Orders.Add(order);
    db.SaveChanges();
}

void QueryOrg(EntityDbContext db)
{
    var root = db.OrgUnits.Where(o => o.Parent == null).Include(o => o.Children).FirstOrDefault();
    PrintOrg(root!);
}

void PrintOrg(OrgUnit org)
{
    PrintOrgEntity(org);
    foreach (var sub in org.Children)
    {
        PrintOrg(sub);
    }
}

void PrintOrgEntity(OrgUnit org)
{
    var o = new
    {
        org.Id,
        org.Name
    };
    System.Console.WriteLine(JsonSerializer.Serialize(o));
}

void InsertOrg(EntityDbContext db)
{
    var root = new OrgUnit
    {
        Name = "Aon1.0",
        Children = new List<OrgUnit>
        {
            new OrgUnit
            {
                Name = "Aon2.1"
            },
            new OrgUnit
            {
                Name = "Aon2.2"
            }
        }
    };
    db.OrgUnits.Add(root);
    db.SaveChanges();
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

void ComplexQueryArticle(EntityDbContext db)
{
    //var articles = db.Articles.Where(a => a.Comments.Any(c => c.Message.Contains("Comment")));
    var articles = db.Comments.Where(c => c.Message.Contains("Comment")).Select(c => c.Article).Distinct();
    foreach (var article in articles)
    {
        PrintArticleEntity(article);
    }
}