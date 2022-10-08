using System.Reflection;
using ddd.usermanager.api;
using ddd.usermanager.domain;
using ddd.usermanager.efcore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(o =>
{
    o.UseSqlite("Data Source=ddd.db");
});

builder.Services.Configure<MvcOptions>(o =>
{
    o.Filters.Add<UnitOfWorkFilter>();
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUserDomainService, UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISmsCodeSender, MockSmsCodeSender>();


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
