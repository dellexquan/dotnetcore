using ddd.usermanager.domain;

namespace ddd.usermanager.api
{
    public class NewUserRequest
    {
        public PhoneNumber PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}