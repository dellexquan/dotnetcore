using Microsoft.AspNetCore.Identity;

namespace aspnetcore.identity;

public class MyUser : IdentityUser<long>
{
    public string? WechatOpenId { get; set; }
}