namespace ddd.usermanager.api;

[AttributeUsage(AttributeTargets.Method)]
public class UnitOfWorkAttribute : Attribute
{
    public Type[] DbContextTypes { get; init; }

    public UnitOfWorkAttribute(params Type[] dbContextTypes)
    {
        this.DbContextTypes = dbContextTypes;
    }
}