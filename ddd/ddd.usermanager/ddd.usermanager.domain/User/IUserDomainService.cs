namespace ddd.usermanager.domain
{
    public interface IUserDomainService
    {
        Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNumber, string password);
        bool IsLockout(User user);
        void AccessFail(User user);
        void ResetAccessFail(User user);
        Task PublishEventAsync(UserAccessResultEvent e);
    }
}