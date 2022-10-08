using ddd.usermanager.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ddd.usermanager.efcore;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext dbContext;
    private readonly IDistributedCache distributedCache;

    public UserRepository(UserDbContext dbContext, IDistributedCache distributedCache)
    {
        this.dbContext = dbContext;
        this.distributedCache = distributedCache;
    }
    public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string msg)
    {
        var user = await FindOneAsync(phoneNumber);
        Guid? userId = null;
        if (user != null)
        {
            userId = user.Id;
        }
        dbContext.UserLoginHistories.Add(new UserLoginHistory(userId, phoneNumber, msg));
    }

    public Task<User?> FindOneAsync(PhoneNumber phoneNumber)
    {
        var user = dbContext.Users.Include(u => u.AccessFail).SingleOrDefault(u => u.PhoneNumber.RegionCode == phoneNumber.RegionCode && u.PhoneNumber.Number == phoneNumber.Number);
        return Task.FromResult(user);
    }

    public Task<User?> FindOneAsync(Guid userId)
    {
        var user = dbContext.Users.Include(u => u.AccessFail).SingleOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }

    public async Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber)
    {
        var cacheKey = GetPhoneCodeCacheKey(phoneNumber);
        var code = await distributedCache.GetStringAsync(cacheKey);
        await distributedCache.RemoveAsync(cacheKey);
        return code;
    }

    private string GetPhoneCodeCacheKey(PhoneNumber phoneNumber)
    {
        var fullNumber = $"{phoneNumber.RegionCode} - {phoneNumber.Number}";
        return $"LoginByPhoneAndCode_Code_{fullNumber}";
    }

    public async Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code)
    {
        var cacheKey = GetPhoneCodeCacheKey(phoneNumber);
        await distributedCache.SetStringAsync(
            cacheKey,
            code,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            }
        );
    }

    public Task AddNewUserAsync(User user)
    {
        dbContext.Users.Add(user);
        return Task.CompletedTask;
    }
}