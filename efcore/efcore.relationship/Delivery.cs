public class Delivery
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Number { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public long OrderId { get; set; }
}