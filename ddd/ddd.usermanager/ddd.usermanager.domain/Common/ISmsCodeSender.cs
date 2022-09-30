namespace ddd.usermanager.domain;
public interface ISmsCodeSender
{
    Task SendAsync(PhoneNumber phoneNumber, string code);
}