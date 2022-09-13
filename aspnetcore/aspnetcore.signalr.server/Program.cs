using aspnetcore.signalr.server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//signalr
//for single version
//builder.Services.AddSignalR()
//for cluster version
builder.Services.AddSignalR().AddStackExchangeRedis("127.0.0.1", options =>
{
    options.Configuration.ChannelPrefix = "Test1_";
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//cors
string[] urls = new[] { "http://localhost:5173" };
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
//cors
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
//signalr
app.MapHub<ChatRoomHub>("/hubs/chatroomhub");

app.MapControllers();

app.Run();
