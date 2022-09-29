namespace ddd.usermanager.domain;
public record UserAccessFail
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    private bool isLockout;
    public DateTime? LockoutEndDate { get; private set; }
    public int AccessFailedCount { get; private set; }

    private UserAccessFail() { }
    public UserAccessFail(User user)
    {
        Id = Guid.NewGuid();
        UserId = user.Id;
        User = user;
    }
    public void Reset()
    {
        isLockout = false;
        LockoutEndDate = null;
        AccessFailedCount = 0;
    }

    public void Fail()
    {
        AccessFailedCount++;
        if (AccessFailedCount >= 3)
        {
            isLockout = true;
            LockoutEndDate = DateTime.Now.AddMinutes(5);
        }
    }

    public bool IsLockOut()
    {
        if (isLockout && LockoutEndDate >= DateTime.Now)
        {
            return true;
        }
        else if (isLockout)
        {
            Reset();
        }

        return false;
    }
}