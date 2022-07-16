using demo15.mailsvc;
using Microsoft.Extensions.DependencyInjection;

const string LOG_FILE_NAME = "mail.ini";

var services = new ServiceCollection();
//services.AddScoped(typeof(IConfigService), s => new IniFileConfigService(LOG_FILE_NAME));
//services.AddEnvVarConfigService();
services.AddIniFileConfigService(LOG_FILE_NAME);
services.AddEnvVarConfigService();
services.AddLayeredConfig();
//services.AddScoped<IConfigService, EnvVarConfigService>();
//services.AddScoped<IMailService, MailService>();
services.AddMailService();
//services.AddScoped<ILogProvider, ConsoleLogProvider>();
services.AddConsoleLog();

using (var sp = services.BuildServiceProvider())
{
    var svc = sp.GetRequiredService<IMailService>();
    svc.Send("Hello", "dellex.quan@aon.com", "Hello World!");
};


