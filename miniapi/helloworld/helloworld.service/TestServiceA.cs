namespace helloworld.Service;

public interface ITestServiceA
{
    string ShowA();
}

public class TestServiceA : ITestServiceA
{
    public TestServiceA()
    {
        System.Console.WriteLine($"{GetType().Name} created.");
    }

    public string ShowA()
    {
        return $"This is from {GetType().FullName} ShowA";
    }
}