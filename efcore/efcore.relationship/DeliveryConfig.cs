using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        //builder.HasOne(d => d.Order).WithOne(o => o.Delivery).HasForeignKey<Delivery>(d => d.OrderId).IsRequired();
    }
}