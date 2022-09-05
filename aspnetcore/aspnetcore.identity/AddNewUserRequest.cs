using System.ComponentModel.DataAnnotations;

namespace aspnetcore.identity;
public class AddNewUserRequest
{
    //[MinLength(3)]
    public string UserName { get; set; } = null!;
    //[EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    //[Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}