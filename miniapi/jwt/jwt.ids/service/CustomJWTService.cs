using System.Security.Claims;
using System.Text;
//using System.Identity.Tokens;

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

        //svar k = Encoding.UTF8.GetBytes("token");
        //var k = Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey);
        //var key = new SymmetricSecurityKey(k);

        return "token";
    }
}