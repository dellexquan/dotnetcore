using System.Text.Json;
using helloworld.Service;

var builder = WebApplication.CreateBuilder(args);

//register swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITestServiceA, TestServiceA>();
builder.Services.AddTransient<ITestServiceB, TestServiceB>();

var app = builder.Build();

//add swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!!");
app.MapGet("api/get-info", () => "This is a get method.");
app.MapPost("api/get-info", () => "This is a post method.");
app.MapPut("api/get-info", () => "This is a put method.");
app.MapDelete("api/get-info", () => "This is a delete method.");

app.MapMethods("api/all-requests", new string[] { "Get", "Post", "Put", "Delete" }, () => "This is for all request.");

app.MapGet("api/get-int", (int i) => $"accept int param: {i}");
app.MapGet("api/get-string", (string s) => $"accept string param: {s}");

app.MapPost("api/post-user", (UserInfo user) => $"The user is {JsonSerializer.Serialize(user)}");

app.MapGet("api/test-service-a", (ITestServiceA svc) => svc.ShowA());
app.MapGet("api/test-service-b", (ITestServiceB svc) => svc.ShowB());
app.MapPost("api/test-serice-b", (ITestServiceB svc) => svc.ShowA());

app.Run();

public class UserInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreateTime { get; set; }
}
