using ddd.usermanager.domain;
using ddd.usermanager.efcore;
using MediatR;

namespace ddd.usermanager.api;

public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
{
    private readonly IUserRepository userRepository;
    private readonly UserDbContext dbContext;

    public UserAccessResultEventHandler(IUserRepository userRepository, UserDbContext dbContext)
    {
        this.userRepository = userRepository;
        this.dbContext = dbContext;
    }

    public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
    {
        await userRepository.AddNewLoginHistoryAsync(notification.phoneNumber, $"Login result: {notification.result}");
        await dbContext.SaveChangesAsync();
    }
}