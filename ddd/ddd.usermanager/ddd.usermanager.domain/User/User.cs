namespace ddd.usermanager.domain;

public record User : IAggregateRoot
{
    public Guid Id { get; init; }
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    private string? passwordHash;
    public UserAccessFail AccessFail { get; private set; } = null!;

    private User() { }

    public User(PhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        PhoneNumber = phoneNumber;
        AccessFail = new UserAccessFail(this);
    }

    public bool HasPassword()
    {
        return !string.IsNullOrEmpty(this.passwordHash);
    }

    public void ChangePassword(string value)
    {
        if (value.Length <= 3)
            throw new ArgumentException("Length should be at least 3.");

        passwordHash = HashHelper.ComputeMd5Hash(value);
    }

    public void ChangePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

}