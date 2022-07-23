using System.Security.Claims;

namespace jwt.ids.service;

public class CustomJWTService : ICustomJWTService
{
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
            new Claim("NickName", user.NickName),
            new Claim("Role", user.RoleList),
            new Claim("Description", user.Description),
            new Claim("Age", user.Age.ToString())
        };

        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

        return "token";
    }
}