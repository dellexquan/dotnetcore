using ddd.usermanager.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ddd.usermanager.efcore;

public class UserAccessFailConfig : IEntityTypeConfiguration<UserAccessFail>
{
    public void Configure(EntityTypeBuilder<UserAccessFail> builder)
    {
        builder.Property("isLockout");
    }
}