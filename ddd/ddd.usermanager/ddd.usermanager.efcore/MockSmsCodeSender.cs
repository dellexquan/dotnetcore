using ddd.usermanager.domain;

namespace ddd.usermanager.efcore;

public class MockSmsCodeSender : ISmsCodeSender
{
    public Task SendAsync(PhoneNumber phoneNumber, string code)
    {
        System.Console.WriteLine($"Send to {phoneNumber.RegionCode} - {phoneNumber.Number}: code [{code}]");
        return Task.CompletedTask;
    }
}