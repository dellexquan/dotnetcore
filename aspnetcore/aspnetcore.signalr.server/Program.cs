using aspnetcore.signalr.server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//signalr
builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//cors
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
//cors
app.UseCors();

app.UseAuthorization();
//signalr
app.MapHub<ChatRoomHub>("/hubs/chatroomht");

app.MapControllers();

app.Run();
