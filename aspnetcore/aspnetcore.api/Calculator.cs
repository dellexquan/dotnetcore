namespace aspnetcore.api;

public interface ICalculator
{
    int Add(int i1, int i2);
}

public class Calculator : ICalculator
{
    public int Add(int i1, int i2)
    {
        return i1 + i2;
    }
}