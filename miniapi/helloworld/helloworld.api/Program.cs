using System.Text.Json;
using helloworld.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

//register swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Dellex MiniApi V1",
        Version = "v1"
    });
    options.SwaggerDoc("v2", new()
    {
        Title = "Dellex MiniApi V2",
        Version = "v2"
    });

    options.OperationFilter<FileUploadOperationFilter>();
});

//register cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//register service
builder.Services.AddTransient<ITestServiceA, TestServiceA>();
builder.Services.AddTransient<ITestServiceB, TestServiceB>();

var app = builder.Build();

//use cors
app.UseCors("CorsPolicy");

//add swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnableTryItOutByDefault();
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dellex MiniApi V1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Dellex MiniApi V2");
});



app.MapGet("/", () => "Hello World!!").WithTags("Index").WithGroupName("v1");

app.MapGet("api/get-info", () => "This is a get method.").WithTags("Info").WithGroupName("v1");
app.MapPost("api/get-info", () => "This is a post method.").WithTags("Info").WithGroupName("v1");
app.MapPut("api/get-info", () => "This is a put method.").WithTags("Info").WithGroupName("v1");
app.MapDelete("api/get-info", () => "This is a delete method.").WithTags("Info").WithGroupName("v1");

app.MapMethods("api/all-requests", new string[] { "Get", "Post", "Put", "Delete" }, () => "This is for all request.").WithGroupName("v2");

app.MapGet("api/get-int", (int i) => $"accept int param: {i}").WithGroupName("v2");
app.MapGet("api/get-string", (string s) => $"accept string param: {s}").WithGroupName("v2");

app.MapPost("api/post-user", (UserInfo user) => $"The user is {JsonSerializer.Serialize(user)}").WithGroupName("v2");

app.MapGet("api/test-service-a", (ITestServiceA svc) => svc.ShowA()).WithTags("Service").WithGroupName("v2");
app.MapGet("api/test-service-b", (ITestServiceB svc) => svc.ShowB()).WithTags("Service").WithGroupName("v2");
app.MapPost("api/test-serice-b", (ITestServiceB svc) => svc.ShowA()).WithTags("Service").WithGroupName("v2");

app.MapPost("api/upload-file", async (HttpRequest req) =>
{
    var form = await req.ReadFormAsync();
    return new JsonResult(new
    {
        Success = true,
        Message = "Upload success.",
        FileName = form.Files.FirstOrDefault()?.FileName
    });
}).Accepts<HttpRequest>("multipart/form-data").WithTags("File").WithGroupName("v1");

app.Run();

public class UserInfo
{
    public UserInfo(int id, DateTime createTime)
    {
        this.Id = id;
        this.CreateTime = createTime;

    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreateTime { get; set; }
}

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        const string FileUploadContentType = "multipart/form-data";

        if (operation.RequestBody == null || !operation.RequestBody.Content.Any(
            x => x.Key.Equals(FileUploadContentType, StringComparison.InvariantCultureIgnoreCase)))
        {
            return;
        }

        if (context.ApiDescription.ParameterDescriptions[0].Type == typeof(HttpRequest))
        {
            operation.RequestBody = new OpenApiRequestBody();
            operation.RequestBody.Description = "File Upload";
            operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType>();
            var mediaType = new OpenApiMediaType();
            var schema = new OpenApiSchema();
            schema.Type = "object";
            schema.Required = new HashSet<string> { "file" };
            schema.Properties = new Dictionary<string, OpenApiSchema>();
            schema.Properties.Add("file", new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });
            mediaType.Schema = schema;
            operation.RequestBody.Content.Add(FileUploadContentType, mediaType);
        };
    }
}
