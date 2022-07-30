public class Order
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Delivery? Delivery { get; set; }
}