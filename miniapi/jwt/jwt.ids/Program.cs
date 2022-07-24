using jwt.ids.service;
using jwt.ids;

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
//register service
builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();
//register config option
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

var app = builder.Build();
//enable swagger
app.UseSwagger();
app.UseSwaggerUI();
//enable cors
app.UseCors("CorsPolicy");

app.MapGet("/", () => "Hello World!");
app.MapPost("auth/login", (string name, string pwd, ICustomJWTService svc) =>
{
    if ("Richard".Equals(name) && "123".Equals(pwd))
    {
        var user = new CurrentUser
        {
            Id = 123,
            Name = "Richard",
            Age = 36,
            NickName = "Teacher Richard",
            Description = ".net developer",
            RoleList = "admin,teacher"
        };
        var token = svc.GetToken(user);
        return new
        {
            result = true,
            token
        };

    }

    return new
    {
        result = false,
        token = ""
    };

});

app.Run();
