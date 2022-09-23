public class Order
{
    public int Id { get; set; }
    public DateTime CreateDateTime { get; set; }
    public double TotalAmount { get; set; }
    public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();

    public void AddDetail(Merchan merchan, int count)
    {
        var detail = Details.SingleOrDefault(x => x.MerchanId == merchan.Id);
        if (detail == null)
        {
            detail = new OrderDetail { MerchanId = merchan.Id, Count = count };
            Details.Add(detail);
        }
        else
        {
            detail.Count += count;
        }
    }
}