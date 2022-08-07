namespace aspnetcore.api;

public class MyDbContext
{
    public static Book? GetById(int id)
    {
        switch (id)
        {
            case 0: return new Book(0, "Book0");
            case 1: return new Book(0, "Book1");
            case 2: return new Book(0, "Book2");
            default: return null;
        }
    }

    public static async Task<Book?> GetByIdAsync(int id)
    {
        return await Task.FromResult<Book?>(GetById(id));
    }
}