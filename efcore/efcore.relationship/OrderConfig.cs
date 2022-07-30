
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //throw new NotImplementedException();
        builder.HasOne(o => o.Delivery).WithOne(d => d.Order).HasForeignKey<Delivery>(o => o.OrderId);
    }
}