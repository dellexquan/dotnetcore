using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        //throw new NotImplementedException();
        builder.HasMany(t => t.Students).WithMany(s => s.Teachers).UsingEntity(j => j.ToTable("Students_Teachers"));
    }
}