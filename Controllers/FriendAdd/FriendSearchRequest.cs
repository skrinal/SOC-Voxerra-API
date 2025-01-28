namespace Voxerra_API.Controllers.FriendAdd;

public class FriendSearchRequest
{
    public int IdOfUser { get; set; }
    public string Search { get; set; } = null!;
}