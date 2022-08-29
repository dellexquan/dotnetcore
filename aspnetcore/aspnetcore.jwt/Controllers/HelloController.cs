using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.jwt.controllers;


[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = "admin")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string Test()
    {
        var name = this.User.FindFirst(ClaimTypes.Name)!.Value;
        var role = this.User.FindFirst(ClaimTypes.Role)!.Value;
        return $"ok user_name: {name}, role: {role}";
    }
}