using jwt.api.models;
using jwt.common;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
//register swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register jwt
var tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
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

var app = builder.Build();
//enalbe auth
app.UseAuthentication();
app.UseAuthorization();
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
