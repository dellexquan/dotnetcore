using demo15.mailsvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class MailServiceExtensions
{
    public static void AddMailService(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();
    }
}