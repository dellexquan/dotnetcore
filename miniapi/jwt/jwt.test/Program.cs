using jwt.db;
using System.Text.Json;
//using jwt.db.model;

using (var db = new JWTDbContext())
{
    // var company = new Company()
    // {
    //     Id = 54,
    //     Name = "Hello 2022",
    //     CreateTime = DateTime.Now,
    //     CreatorId = 1,
    //     LastModifierId = 1,
    //     LastModifyTime = DateTime.Now
    // };

    // db.Company!.Add(company);
    // db.SaveChanges();
    var company = db!.Company!.Find(54);
    System.Console.WriteLine(JsonSerializer.Serialize(company));

}