using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookEntityConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.Id);
        builder.Property(book => book.Title).IsRequired();
        builder.Property(book => book.AuthorName).IsRequired();
        builder.ToTable("Books");
    }
}

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(person => person.Id);
        builder.Property(person => person.BirthPlace).IsRequired();
        builder.Property(person => person.Age).IsRequired();
        builder.Property(person => person.Name).IsRequired();
        builder.ToTable("Persons");
    }
}