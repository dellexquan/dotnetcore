using MediatR;

public record NewUserNotification(string UserName, DateTime CreateTime) : INotification;