using demo15.logsvc;
using demo15.configsvc;

namespace demo15.mailsvc;

public class MailService : IMailService
{
    private readonly ILogProvider log;
    private readonly IConfigReader config;

    public MailService(ILogProvider log, IConfigReader config)
    {
        this.log = log;
        this.config = config;
    }

    public void Send(string title, string to, string body)
    {
        this.log.LogInfo("Prepare to send email...");
        var smtpServer = this.config.GetValue("SmptServer");
        var userName = this.config.GetValue("UserName");
        var pwd = this.config.GetValue("Password");
        Console.WriteLine($"Smpt Server: {smtpServer}");
        Console.WriteLine($"User Name: {userName}");
        Console.WriteLine($"Password: {pwd}");
        Console.WriteLine($"Send mail: {title} {to}");
        this.log.LogInfo("Mail sended!");
    }
}