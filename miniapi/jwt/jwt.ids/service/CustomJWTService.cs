using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using jwt.common;

namespace jwt.ids.service;

public class CustomJWTService : ICustomJWTService
{
    private readonly JWTTokenOptions jwtTokenOptions;

    /// <summary>
    /// Get Token by User
    /// </summary>
    /// <param name="user">user</param>
    /// <returns>token</returns>
    public string GetToken(CurrentUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("NickName", user?.NickName ?? ""),
            new Claim("Role", user?.RoleList ?? ""),
            new Claim("Description", user?.Description ?? ""),
            new Claim("Age", user?.Age.ToString() ?? "0")
        };

        var securityKey = jwtTokenOptions.SecurityKey;
        var bytesKey = Encoding.UTF8.GetBytes(securityKey!);
        var key = new SymmetricSecurityKey(bytesKey);

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtTokenOptions.Issuer,
            audience: jwtTokenOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: creds
        );

        var returnToken = new JwtSecurityTokenHandler().WriteToken(token);

        return returnToken;
    }

    public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
    {
        this.jwtTokenOptions = jwtTokenOptions.CurrentValue;
    }
}