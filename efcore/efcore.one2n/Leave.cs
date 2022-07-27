public class Leave
{
    public long Id { get; set; }
    public long RequesterId { get; set; }
    public User Requester { get; set; } = null!;
    public long? ApproverId { get; set; }
    public User? Approver { get; set; }
    public string? Remarks { get; set; }
}