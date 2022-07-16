namespace demo15.configsvc;

class LayeredConfigReader : IConfigReader
{
    private readonly IEnumerable<IConfigService> services;

    public LayeredConfigReader(IEnumerable<IConfigService> services)
    {
        this.services = services;
    }

    public string? GetValue(string name)
    {
        string? value = null;
        foreach (var services in services)
        {
            var newValue = services.GetValue(name);
            if (newValue != null)
            {
                value = newValue;
            }
        }
        return value;
    }
}