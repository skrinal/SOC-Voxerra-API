namespace Voxerra_API.Entities;

public class TblGroups
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AvatarSourceName  { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public TblUser Creator { get; set; }
    public ICollection<TblGroupMembers> Members { get; set; }
    public ICollection<TblGroupMessages> Messages { get; set; }
}