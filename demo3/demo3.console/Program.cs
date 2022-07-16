// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
public class Product
{
    public string Name { get; set; } = null!;
    public DateTime Expiry { get; set; }
    public string[] Sizes { get; set; } = null!;
}

public class Application
{
    public static void Main()
    {
        var product = new Product();
        product.Name = "Apple";
        product.Expiry = new DateTime(2008, 12, 28);
        product.Sizes = new string[] { "Small" };
        string json = JsonConvert.SerializeObject(product);

        Console.WriteLine(json);

    }
}


