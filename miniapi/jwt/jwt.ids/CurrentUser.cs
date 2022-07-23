namespace jwt.ids;

public class CurrentUser
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string? NickName { get; set; }
    public string? Description { get; set; }
    public string? RoleList { get; set; }
}