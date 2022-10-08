using ddd.usermanager.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ddd.usermanager.efcore;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(x => x.PhoneNumber, nb =>
        {
            nb.Property(x => x.RegionCode).HasMaxLength(5).IsUnicode(false);
            nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
        });
        builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
        builder.HasOne(x => x.AccessFail).WithOne(x => x.User).HasForeignKey<UserAccessFail>(x => x.UserId);
    }
}