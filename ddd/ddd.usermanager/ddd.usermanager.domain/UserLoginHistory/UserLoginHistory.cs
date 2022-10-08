namespace ddd.usermanager.domain;

public record UserLoginHistory : IAggregateRoot
{
    public long Id { get; init; }
    public Guid? UserId { get; init; }
    public PhoneNumber PhoneNumber { get; init; } = null!;
    public DateTime CreateTime { get; init; }
    public string Message { get; init; } = null!;

    private UserLoginHistory() { }
    public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
    {
        this.UserId = userId;
        this.PhoneNumber = phoneNumber;
        this.CreateTime = DateTime.Now;
        this.Message = message;
    }
}