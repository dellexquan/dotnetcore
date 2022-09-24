using MediatR;

public class NewUserHander : NotificationHandler<NewUserNotification>
{
    protected override void Handle(NewUserNotification notification)
    {
        System.Console.WriteLine($"New user {notification.UserName} created at {notification.CreateTime}.");
    }
}