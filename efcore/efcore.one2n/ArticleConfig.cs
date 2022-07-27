using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class AriticleConfig : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(a => a.Content).IsRequired().IsUnicode();
        builder.Property(a => a.Title).IsRequired().IsUnicode();
        builder.HasMany(a => a.Comments).WithOne(c => c.Article).HasForeignKey(c => c.ArticleId).IsRequired();
    }
}