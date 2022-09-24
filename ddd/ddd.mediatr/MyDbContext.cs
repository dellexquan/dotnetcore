using MediatR;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    private readonly IMediator mediator;
    public MyDbContext(IMediator mediator)
    {
        this.mediator = mediator;
    }
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=ddd.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = this.ChangeTracker.Entries<IDomainEvents>().Where(x => x.Entity.GetDomainEvents().Any());
        var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();
        domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

}