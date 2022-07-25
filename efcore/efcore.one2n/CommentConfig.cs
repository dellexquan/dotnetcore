using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Message).IsRequired().IsUnicode();
        builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).IsRequired();
    }
}