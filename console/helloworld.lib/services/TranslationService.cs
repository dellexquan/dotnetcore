using System.Reflection;
using System.Text.Json;
using helloworld.lib.models;
using Microsoft.Extensions.Logging;

namespace helloworld.lib.services;

public class TranslationService : ITransaltionService
{
    private readonly ILogger<Translation> logger;
    private readonly List<Translation> translations;

    public TranslationService(ILogger<Translation> logger)
    {
        this.logger = logger;
        this.translations = LoadTranslations("translations/trans.json");
    }

    private string GetPath(string fileName)
    {
        return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, fileName);
    }

    private List<Translation> LoadTranslations(string fileName)
    {
        try
        {
            var path = GetPath(fileName);
            var content = File.ReadAllText(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var translations = JsonSerializer.Deserialize<List<Translation>>(content, options);
            if (translations == null)
                throw new ArgumentException("The translation file is empty.");
            return translations;
        }
        catch
        {
            this.logger.LogError("Fail to load translation json!");
            throw;
        }
    }

    private string LookUpTranslation(string key, string language)
    {
        return translations.First(t => t.Language == language).Translations[key];
    }

    public string Greeting(string language)
    {
        return LookUpTranslation("Greeting", language);
    }
}