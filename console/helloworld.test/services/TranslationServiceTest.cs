using helloworld.lib.models;
using helloworld.lib.services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace helloworld.test.services;

public class TranslationServiceTest
{
    [Fact]
    public void Greeting_InEnglish()
    {
        ILogger<Translation> logger = new NullLogger<Translation>();
        var translationService = new TranslationService(logger);

        var expected = "Hello World";
        var actual = translationService.Greeting("en");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Greeting_InSpanish()
    {
        ILogger<Translation> logger = new NullLogger<Translation>();
        var translationService = new TranslationService(logger);

        var expected = "Hola Mundo";
        var actual = translationService.Greeting("es");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Greeting_InFrench()
    {
        ILogger<Translation> logger = new NullLogger<Translation>();
        var translationService = new TranslationService(logger);

        var expected = "Salut tout le monde";
        var actual = translationService.Greeting("fr");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Greeting_Invalid()
    {
        ILogger<Translation> logger = new NullLogger<Translation>();
        var translationService = new TranslationService(logger);

        Assert.Throws<InvalidOperationException>(
            () =>
            {
                translationService.Greeting("cn");
            }
        );
    }
}