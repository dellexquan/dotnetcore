using MediatR;

public record UserNameChangeNotification(string OldUserName, string NewUserName) : INotification;