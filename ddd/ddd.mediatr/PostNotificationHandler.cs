using MediatR;

public class PostNotificationHandler : NotificationHandler<PostNotification>
{
    protected override void Handle(PostNotification notification)
    {
        System.Console.WriteLine("Received notification: " + notification.Body);
    }
}