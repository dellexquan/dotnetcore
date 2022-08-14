using aspnetcore.api;
using aspnetcore.cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICalculator, Calculator>();
//add cache helper
builder.Services.AddCacheHelper();

var redis_host = builder.Configuration.GetSection("Redis").GetValue<string>("Host");
var redis_instance_name = builder.Configuration.GetSection("Redis").GetValue<string>("InstanceName");
System.Console.WriteLine($"redis_host: {redis_host}");
System.Console.WriteLine($"redis_instance_name: {redis_instance_name}");
//config redis
builder.Services.AddStackExchangeRedisCache(opitons =>
{
    opitons.Configuration = redis_host;
    opitons.InstanceName = redis_instance_name;
});

string[] urls = new[] { "http://localhost:8080" };
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
        .WithOrigins(urls)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
