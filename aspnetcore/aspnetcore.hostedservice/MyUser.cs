using Microsoft.AspNetCore.Identity;

namespace aspnetcore.hostedservice;

public class MyUser : IdentityUser<long>
{
    public string? WechatOpenId { get; set; }
}