using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property("passwordHash");
        builder.Property(u => u.Remark).HasField("remark");
        builder.Ignore(u => u.Tag);
        builder.Property(u => u.Gender).HasConversion<string>();
    }
}