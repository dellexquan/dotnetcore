using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.identity.controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DemoController : ControllerBase
{
    private readonly UserManager<MyUser> userManager;
    private readonly RoleManager<MyRole> roleManager;

    public DemoController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
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

    [HttpPost]
    public async Task<ActionResult> CheckPwd(CheckPwdRequest req)
    {
        var userName = req.UserName;
        var pwd = req.Password;
        var user = await userManager.FindByNameAsync(userName);
        if (user == null) return NotFound($"User not existed.");
        var isLock = await userManager.IsLockedOutAsync(user);
        if (isLock) return BadRequest("User is locked.");
        var isValid = await userManager.CheckPasswordAsync(user, pwd);
        if (isValid)
        {
            await userManager.ResetAccessFailedCountAsync(user);
            return Ok("Login success.");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
            return BadRequest("User name or password is incorrect!");
        }
    }

    [HttpPost]
    public async Task<ActionResult> SendResetPasswordToken(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null) return NotFound($"User not existed.");
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        System.Console.WriteLine($"token is {token}");
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> ResetPassword(string userName, string token, string newPassword)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return NotFound("User not existed.");
        }
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            await userManager.ResetAccessFailedCountAsync(user);
            return Ok("Password reset success.");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
            return BadRequest("Reset password failed.");
        }
    }
}