namespace helloworld.lib.models;
public class Translation
{
    public string Language { get; set; } = null!;
    public Dictionary<string, string> Translations { get; set; } = null!;
}