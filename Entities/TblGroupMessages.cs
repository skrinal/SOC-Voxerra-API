namespace Voxerra_API.Entities;

public class TblGroupMessages
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SendDateTime { get; set; }
    
    /*public bool IsRead { get; set; }*/
    public TblGroups Group { get; set; }
    public TblUser Sender { get; set; }
}