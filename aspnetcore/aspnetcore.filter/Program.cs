using aspnetcore.filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MvcOptions>(
    opt =>
    {
        opt.Filters.Add<MyActionFilter>();
        opt.Filters.Add<MyExceptionFilter>();
        opt.Filters.Add<LogExceptionFilter>();
    }
);

builder.Services.AddDbContext<MyDbContext>(opt =>
{
    var connStr = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
    opt.UseSqlite(connStr);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
