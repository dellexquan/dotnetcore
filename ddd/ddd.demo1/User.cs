public class User
{
    public Guid Id { get; init; }
    public DateTime CreateDateTime { get; init; }
    public string UserName { get; private set; } = null!;
    public int Credit { get; private set; }
    private string? passwordHash;

    private string? remark;
    public string? Remark
    {
        get
        {
            return this.remark;
        }
    }
    public string? Tag { get; set; }

    public User(string userName)
    {
        this.Id = new Guid();
        this.UserName = userName;
        this.CreateDateTime = DateTime.Now;
        this.Credit = 10;
    }

    private User()
    {

    }

    public void ChangeUserName(string userName)
    {
        if (userName.Length > 5)
        {
            System.Console.WriteLine("Too long! Length max is 5!");
            throw new ArgumentException("Too long! Length max is 5!");
        }
        this.UserName = userName;
    }

    public void ChangePassword(string password)
    {
        if (password.Length < 3)
        {
            System.Console.WriteLine("Too short! Length min is 3!");
            throw new ArgumentException("Too short! Length min is 3!");
        }
        this.passwordHash = HashHelper.GetMD5Hash(password);
    }
}