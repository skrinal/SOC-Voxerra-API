namespace Voxerra_API.Entities;

public class TblGroupMembers
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedAt { get; set; }
    
    public TblGroups Group { get; set; }
    public TblUser User { get; set; }
}