namespace SystemServices;
using Microsoft.Extensions.Logging;

class Test2
{
    private readonly ILogger<Test2> logger;
    public Test2(ILogger<Test2> logger)
    {
        this.logger = logger;
    }

    public void Test()
    {
        logger.LogInformation("That is an info message.");
        logger.LogDebug("That is a debug message.");
        logger.LogWarning("That is a warning message.");
        logger.LogError("That is an error message.");

        try
        {
            var content = File.ReadAllText("1.txt");
            logger.LogInformation(content);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fail to read from the file.");
        }
    }

}