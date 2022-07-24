using jwt.api.models;
using jwt.common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//register swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
    {
        Description = "Please input token, the format is Bearer xxxxxx (P.S. don't miss the blank in the middle.)",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    var reference = new OpenApiReference()
    {
        Type = ReferenceType.SecurityScheme,
        Id = JwtBearerDefaults.AuthenticationScheme
    };

    var scheme = new OpenApiSecurityScheme()
    {
        Reference = reference
    };
    var requirement = new OpenApiSecurityRequirement();
    requirement.Add(scheme, new string[] { });
    options.AddSecurityRequirement(requirement);
});

//register jwt
var tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
builder.Services.AddAuthorization(options => { })
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey ?? "")),
        AudienceValidator = (x, y, z) =>
        {
            return true;
        },
        LifetimeValidator = (notBefore, expires, securityToken, ValidationParamters) =>
        {
            return true;
        }
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
app.MapGet("api/get-user", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] () =>
{
    return new User
    {
        Id = 123,
        Name = "Dellex Quan",
        CreateTime = DateTime.Now
    };
});



app.Run();
