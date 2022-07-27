using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LeaveConfig : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.HasOne<User>(l => l.Requester).WithMany().HasForeignKey(l => l.RequesterId).IsRequired();
        builder.HasOne<User>(l => l.Approver).WithMany().HasForeignKey(l => l.ApproverId);
    }
}