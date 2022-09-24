using MediatR;

public class UserNameChangeHandler : NotificationHandler<UserNameChangeNotification>
{
    protected override void Handle(UserNameChangeNotification notification)
    {
        System.Console.WriteLine($"Change user name from {notification.OldUserName} to {notification.NewUserName}.");
    }
}