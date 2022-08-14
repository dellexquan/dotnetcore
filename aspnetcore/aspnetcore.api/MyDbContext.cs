namespace aspnetcore.api;

public class MyDbContext
{
    public static SimpleBook? GetById(int id)
    {
        switch (id)
        {
            case 0: return new SimpleBook(0, "Book0");
            case 1: return new SimpleBook(0, "Book1");
            case 2: return new SimpleBook(0, "Book2");
            default: return null;
        }
    }

    public static async Task<SimpleBook?> GetByIdAsync(int id)
    {
        return await Task.FromResult<SimpleBook?>(GetById(id));
    }
}