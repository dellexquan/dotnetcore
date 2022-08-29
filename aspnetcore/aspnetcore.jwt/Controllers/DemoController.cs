using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace aspnetcore.jwt.controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly IOptionsSnapshot<JWTSettings> jwtSettingsOption;

    public DemoController(IOptionsSnapshot<JWTSettings> jwtSettingsOption)
    {
        this.jwtSettingsOption = jwtSettingsOption;
    }

    [HttpPost]
    public ActionResult<string> Login(string userName, string password)
    {
        if (userName == "admin" && password == "123")
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
            claims.Add(new Claim(ClaimTypes.Name, "admin"));
            claims.Add(new Claim(ClaimTypes.Role, "admin"));

            var jwt = GetJwt(claims);
            return jwt;
        }
        else
            return NotFound("Invalid user.");
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