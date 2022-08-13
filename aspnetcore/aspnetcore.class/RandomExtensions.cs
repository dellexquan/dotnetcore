namespace aspnetcore.common;
public static class RandomExtensions
{
    public static double NextDouble(this Random random, double minVal, double maxVal)
    {
        if (minVal >= maxVal)
        {
            throw new ArgumentOutOfRangeException(nameof(minVal), "Min value cannot be greater than max value.");
        }
        var x = random.NextDouble();
        return x * maxVal + (1 - x) * minVal;
    }
}