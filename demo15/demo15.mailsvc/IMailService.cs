namespace demo15.mailsvc;

public interface IMailService
{
    public void Send(string title, string to, string body);
}