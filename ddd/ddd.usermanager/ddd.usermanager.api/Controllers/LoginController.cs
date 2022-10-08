using ddd.usermanager.domain;
using ddd.usermanager.efcore;
using Microsoft.AspNetCore.Mvc;

namespace ddd.usermanager.api;

[ApiController]
[Route("api/[controller]/[action]")]
public class LoginController : ControllerBase
{
    private readonly IUserDomainService userDomianService;

    public LoginController(IUserDomainService userDomianService)
    {
        this.userDomianService = userDomianService;
    }
    [HttpPost]
    [UnitOfWork(typeof(UserDbContext))]
    public async Task<IActionResult> LoginByPhoneAndPwd(LoginByPhoneAndPwdRequest req)
    {
        if (req.Password.Length < 3) return BadRequest("Password length should not be less than 3.");
        var phoneNum = req.PhoneNumber;
        var result = await userDomianService.CheckLoginAsync(phoneNum, req.Password);
        switch (result)
        {
            case UserAccessResult.OK:
                return Ok("Login success!");
            case UserAccessResult.Lockout:
                return BadRequest("The account has been locked!");
            case UserAccessResult.PhoneNumberNotFound:
            case UserAccessResult.NoPassword:
            case UserAccessResult.PasswordError:
                return BadRequest("Phone number or password is incorrect!");
            default:
                throw new ApplicationException("Login result is unknown!");
        }
    }
}