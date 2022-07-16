using System.Xml;
using Microsoft.Extensions.Configuration;

class FxConfigProvider : FileConfigurationProvider
{
    public FxConfigProvider(FxConfigSource source) : base(source)
    {

    }

    public override void Load(Stream stream)
    {
        this.Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(stream);
        var csNodes = xmlDoc.SelectNodes("//connectionStrings/add");
        foreach (var node in csNodes!.Cast<XmlNode>())
        {
            var name = node!.Attributes!["name"]!.Value;
            var connectionString = node!.Attributes!["connectionString"]!.Value;
            var providerName = node!.Attributes!["providerName"]!.Value;

            Data[$"{name}:connectionString"] = connectionString;
            Data[$"{name}:providerName"] = providerName;
        }

        var asNodes = xmlDoc.SelectNodes("//appSettings/add");
        foreach (var node in asNodes!.Cast<XmlNode>())
        {
            var key = node!.Attributes!["key"]!.Value; //.Replace(".", ":");
            var value = node!.Attributes["value"]!.Value;

            Data[key] = value;
        }
    }
}