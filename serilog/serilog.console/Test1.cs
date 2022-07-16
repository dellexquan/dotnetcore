using Microsoft.Extensions.Logging;

class Test1
{
    private readonly ILogger<Test1> logger;
    public Test1(ILogger<Test1> logger)
    {
        this.logger = logger;
    }

    public void Test()
    {
        logger.LogInformation("This is an info message.");
        logger.LogDebug("This is a debug message.");
        logger.LogWarning("This is a warning message.");
        logger.LogError("This is an error message.");
        // var person = new { Id = 3, Name = "Zack" };
        logger.LogWarning("New user: {@person}", new { Id = 3, Name = "Zack" });
        // logger.LogWarning($"New user: {person}");

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