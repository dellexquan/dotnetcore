using MediatR;

namespace ddd.usermanager.domain;
public record UserAccessResultEvent(PhoneNumber phoneNumber, UserAccessResult result) : INotification;