using System.IO;
using System.Linq;

namespace demo15.configsvc;

public class IniFileConfigService : IConfigService
{
    public string FileName { get; set; }

    public IniFileConfigService(string fileName)
    {
        this.FileName = fileName;
    }

    public string? GetValue(string name)
    {
        var query = File.ReadAllLines(this.FileName)
                        .Select(s => s.Split('='))
                        .Select(s => new { Key = s[0], Value = s[1] });
        return query.SingleOrDefault(s => s.Key == name)?.Value;
    }
}