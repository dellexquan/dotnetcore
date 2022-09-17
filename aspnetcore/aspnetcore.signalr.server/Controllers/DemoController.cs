using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aspnetcore.identity;
using aspnetcore.jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace aspnetcore.signalr.server;


[Route("api/[controller]/[action]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly IOptionsSnapshot<JWTSettings> jwtSettingsOption;
    private readonly UserManager<MyUser> userManager;
    private readonly RoleManager<MyRole> roleManager;
    private readonly IHubContext<ChatRoomHub> hubContext;

    public DemoController(
        IOptionsSnapshot<JWTSettings> jwtSettingsOption,
        UserManager<MyUser> userManager,
        RoleManager<MyRole> roleManager,
        IHubContext<ChatRoomHub> hubContext)
    {
        this.jwtSettingsOption = jwtSettingsOption;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.hubContext = hubContext;
    }

    [HttpPost]
    public async Task<ActionResult> AddUser(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new MyUser { UserName = userName };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return BadRequest("Create user failed.");
            await hubContext.Clients.All.SendAsync("ReceivePublicMessage", $"Welcome new user {userName}!");
            return Ok();
        }

        return BadRequest("User name existed!");
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login2(LoginRequest req)
    {
        return await Login(req.UserName, req.Password);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return NotFound("Invalid user or password!");
        }

        if (await userManager.CheckPasswordAsync(user, password))
        {
            await userManager.ResetAccessFailedCountAsync(user);
            //user.JWTVersion++;
            //await userManager.UpdateAsync(user);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwt = GetJwt(claims);
            return jwt;

        }

        return NotFound("Invalid user or password!");
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateAdminRoleAndRootUser()
    {
        var isAdminExisted = await roleManager.RoleExistsAsync("admin");
        if (!isAdminExisted)
        {
            var role = new MyRole { Name = "admin" };
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded) return BadRequest("Create role failed.");
        }
        var user = await userManager.FindByNameAsync("dellex");
        if (user == null)
        {
            user = new MyUser { UserName = "dellex" };
            var result = await userManager.CreateAsync(user, "123456");
            if (!result.Succeeded) return BadRequest("Create user failed.");
        }
        var isRootUserAdmin = await userManager.IsInRoleAsync(user, "admin");
        if (!isRootUserAdmin)
        {
            var result = await userManager.AddToRoleAsync(user, "admin");
        }

        return "ok";
    }

    private string GetJwt(List<Claim> claims)
    {
        var secKey = jwtSettingsOption.Value.SecKey;
        var expire = DateTime.Now.AddSeconds(jwtSettingsOption.Value.ExpireSeconds);
        var secBytes = Encoding.UTF8.GetBytes(secKey);
        var key = new SymmetricSecurityKey(secBytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}