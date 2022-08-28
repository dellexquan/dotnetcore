using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var jwt = GenerateJwtToken();
System.Console.WriteLine(jwt);
var segments = jwt.Split('.');
var head = JwtDecode(segments[0]);
var payload = JwtDecode(segments[1]);
System.Console.WriteLine("--head--");
System.Console.WriteLine(head);
System.Console.WriteLine("--payload--");
System.Console.WriteLine(payload);

var claimsPrincipal = ValidateJwt(jwt);
System.Console.WriteLine("--validate signature--");
foreach (var claim in claimsPrincipal.Claims)
{
    System.Console.WriteLine($"{claim.Type}={claim.Value}");
}

static string GetKey()
{
    return "fasdfa#123214324&adsfkjdsfk@02302";
}

static ClaimsPrincipal ValidateJwt(string jwt)
{
    var secKey = GetKey();
    var tokenHandler = new JwtSecurityTokenHandler();
    var valParam = new TokenValidationParameters();
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
    valParam.IssuerSigningKey = securityKey;
    valParam.ValidateIssuer = false;
    valParam.ValidateAudience = false;
    return tokenHandler.ValidateToken(jwt, valParam, out SecurityToken secToken);
}

static string JwtDecode(string jwt)
{
    jwt = jwt.Replace('-', '+').Replace('_', '/');
    switch (jwt.Length % 4)
    {
        case 2:
            jwt += "==";
            break;
        case 3:
            jwt += "=";
            break;
    }
    var bytes = Convert.FromBase64String(jwt);
    return Encoding.UTF8.GetString(bytes);
}


static string GenerateJwtToken()
{
    var claims = new List<Claim>();
    claims.Add(new Claim(ClaimTypes.NameIdentifier, "6"));
    claims.Add(new Claim(ClaimTypes.Name, "dellex"));
    claims.Add(new Claim(ClaimTypes.Role, "User"));
    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
    claims.Add(new Claim("PassPort", "E90000082"));
    var key = GetKey();
    var expires = DateTime.Now.AddDays(1);
    var secBytes = Encoding.UTF8.GetBytes(key);
    var secKey = new SymmetricSecurityKey(secBytes);
    var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
    var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expires, signingCredentials: credentials);
    return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
}