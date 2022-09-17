using aspnetcore.identity;
using aspnetcore.jwt;
using aspnetcore.signalr.server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var signalrUrl = "/hubs/chatroomhub";

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


builder.Services.AddDbContext<MyDbContext>(
    opt =>
    {
        var connStr = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
        opt.UseSqlite(connStr);
    }
);
builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<MyUser>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    }
);
var idBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
idBuilder.AddEntityFrameworkStores<MyDbContext>()
        .AddDefaultTokenProviders()
        .AddRoleManager<RoleManager<MyRole>>()
        .AddUserManager<UserManager<MyUser>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
});
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(
    opt =>
    {
        var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
        var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecKey);
        var key = new SymmetricSecurityKey(keyBytes);
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key
        };
        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments(signalrUrl)))
                    context.Token = accessToken;
                return Task.CompletedTask;
            }
        };
    }
);

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

app.UseAuthentication();

app.UseAuthorization();
//signalr
app.MapHub<ChatRoomHub>(signalrUrl);

app.MapControllers();

app.Run();
