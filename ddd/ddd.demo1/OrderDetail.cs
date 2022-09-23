public class OrderDetail
{
    public int Id { get; set; }
    public Order Order { get; set; } = null!;
    //public Merchan Merchan { get; set; } = null!;
    public long MerchanId { get; set; }
    public int Count { get; set; }
}