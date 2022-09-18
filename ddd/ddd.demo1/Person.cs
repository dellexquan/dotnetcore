public class Person
{
    public long Id { get; set; }

    private string name = null!;

    public string Name
    {
        get
        {
            System.Console.WriteLine("get is called.");
            return this.name;
        }
        set
        {
            System.Console.WriteLine("set is called.");
            this.name = value;
        }
    }
}