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
}