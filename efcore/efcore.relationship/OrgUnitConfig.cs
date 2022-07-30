using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrgUnitConifg : IEntityTypeConfiguration<OrgUnit>
{
    public void Configure(EntityTypeBuilder<OrgUnit> builder)
    {
        builder.Property(o => o.Name).IsRequired().IsUnicode();
        builder.HasOne(o => o.Parent).WithMany(p => p.Children); 
    }
}