using MediatR;

public record PostNotification(string Body) : INotification;