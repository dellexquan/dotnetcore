using ddd.usermanager.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ddd.usermanager.efcore;

public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
{
    public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
    {
        builder.OwnsOne(x => x.PhoneNumber, nb =>
        {
            nb.Property(x => x.RegionCode).HasMaxLength(5).IsUnicode(false);
            nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
        });
    }
}