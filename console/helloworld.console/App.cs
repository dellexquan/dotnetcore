using helloworld.lib.services;

public class App
{
    private readonly ITransaltionService transaltionService;
    private string lang = "en";

    public App(ITransaltionService transaltionService)
    {
        this.transaltionService = transaltionService;
    }
    public void Run(string[] args)
    {
        InitCommand(args);
        System.Console.WriteLine(transaltionService.Greeting(lang));
    }

    private void InitCommand(string[] args)
    {
        foreach (var arg in args)
        {
            if (arg.StartsWith("lang"))
            {
                lang = arg.Split('=')[1];
                break;
            }
        }
    }
}