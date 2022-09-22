using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BlogConfig : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        //builder.OwnsOne(b => b.Title);
        //builder.OwnsOne(b => b.Body);

        builder.OwnsOne(b => b.Title, nb =>
        {
            nb.Property(g => g.Chinese).HasMaxLength(255).IsUnicode(true);
            nb.Property(g => g.English).HasMaxLength(255).IsUnicode(false);
        });

        builder.OwnsOne(b => b.Body, nb =>
        {
            nb.Property(g => g.Chinese).HasMaxLength(255).IsUnicode(true);
            nb.Property(g => g.English).HasMaxLength(255).IsUnicode(false);
        });
    }
}