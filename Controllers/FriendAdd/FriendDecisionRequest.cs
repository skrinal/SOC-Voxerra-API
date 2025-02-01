namespace Voxerra_API.Controllers.FriendAdd;

public class FriendDecisionRequest
{
    public int UserRequestToId { get; set; }
    public int UserRequestFromId { get; set; }
    public bool Decision { get; set; }
}