namespace jwt.db.model;

public class SysUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public int Status { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? QQ { get; set; }
}