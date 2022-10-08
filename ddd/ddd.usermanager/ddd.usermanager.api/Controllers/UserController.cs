using ddd.usermanager.domain;
using ddd.usermanager.efcore;
using Microsoft.AspNetCore.Mvc;

namespace ddd.usermanager.api;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    [HttpPost]
    [UnitOfWork(typeof(UserDbContext))]
    public async Task<IActionResult> AddNewUser(NewUserRequest req)
    {
        if (req.Password.Length < 3) return BadRequest("Password length should not be less than 3.");
        var user = await userRepository.FindOneAsync(req.PhoneNumber);
        if (user != null)
            return BadRequest("Phone number has been existed!");
        var newUser = new domain.User(req.PhoneNumber);
        newUser.ChangePassword(req.Password);
        await userRepository.AddNewUserAsync(newUser);
        return Ok("Add success!");
    }
}