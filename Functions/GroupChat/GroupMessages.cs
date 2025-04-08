namespace Voxerra_API.Functions.GroupChat;

public class GroupMessages
{
    public int Id { get; set; }
    public int FromUserId { get; set; }
    public int ToGroupId { get; set; }
    public string Content { get; set; }
    public DateTime SendDateTime { get; set; }
}