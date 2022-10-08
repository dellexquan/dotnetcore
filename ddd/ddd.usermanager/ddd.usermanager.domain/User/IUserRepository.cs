namespace ddd.usermanager.domain;

public interface IUserRepository
{
    Task<User?> FindOneAsync(PhoneNumber phoneNumber);
    Task<User?> FindOneAsync(Guid userId);
    Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string msg);
    Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code);
    Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber);
    Task AddNewUserAsync(User user);
}