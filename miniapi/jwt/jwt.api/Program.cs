using jwt.api.models;

var builder = WebApplication.CreateBuilder(args);
//register swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();
//enable swagger
app.UseSwagger();
app.UseSwaggerUI();
//enable cors
app.UseCors("CorePolicy");

app.MapGet("/", () => "Hello World!");
app.MapGet("api/get-user", () =>
{
    return new User
    {
        Id = 123,
        Name = "Dellex Quan",
        CreateTime = DateTime.Now
    };
});

app.Run();
