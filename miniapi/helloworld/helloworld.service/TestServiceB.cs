namespace helloworld.Service;

public interface ITestServiceB
{
    string ShowB();
    string ShowA();
}

public class TestServiceB : ITestServiceB
{
    private readonly ITestServiceA testServiceA;

    public TestServiceB(ITestServiceA testServiceA)
    {
        System.Console.WriteLine($"{GetType().Name} created.");
        this.testServiceA = testServiceA;
    }

    public string ShowB()
    {
        return $"This is from {GetType().FullName} ShowB";
    }

    public string ShowA()
    {
        return testServiceA.ShowA();
    }
}