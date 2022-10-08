using ddd.usermanager.domain;

namespace ddd.usermanager.api;

public class LoginByPhoneAndPwdRequest
{
    public string Password { get; set; } = null!;
    public PhoneNumber PhoneNumber { get; set; } = null!;
}
