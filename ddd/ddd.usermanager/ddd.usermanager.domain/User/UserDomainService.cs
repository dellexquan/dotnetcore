using MediatR;

namespace ddd.usermanager.domain;

public class UserDomainService : IUserDomainService
{
    private readonly IMediator mediatr;
    private readonly IUserRepository userRepository;
    private readonly ISmsCodeSender smsSender;

    public UserDomainService(IMediator mediatr, IUserRepository userRepository, ISmsCodeSender smsSender)
    {
        this.mediatr = mediatr;
        this.userRepository = userRepository;
        this.smsSender = smsSender;
    }

    public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNumber, string password)
    {
        var user = await userRepository.FindOneAsync(phoneNumber);
        UserAccessResult result;
        if (user == null)
            result = UserAccessResult.PhoneNumberNotFound;
        else if (IsLockout(user))
            result = UserAccessResult.Lockout;
        else if (!user.HasPassword())
            result = UserAccessResult.NoPassword;
        else if (user.CheckPassword(password))
            result = UserAccessResult.OK;
        else
            result = UserAccessResult.PasswordError;

        if (user != null)
        {
            if (result == UserAccessResult.OK)
                ResetAccessFail(user);
            else
                AccessFail(user);
        }

        var e = new UserAccessResultEvent(phoneNumber, result);
        await PublishEventAsync(e);

        return result;
    }

    public async Task PublishEventAsync(UserAccessResultEvent e)
    {
        await mediatr.Publish(e);
    }

    public void ResetAccessFail(User user)
    {
        user.AccessFail.Reset();
    }

    public void AccessFail(User user)
    {
        user.AccessFail.Fail();
    }

    public bool IsLockout(User user)
    {
        return user.AccessFail.IsLockOut();
    }
}