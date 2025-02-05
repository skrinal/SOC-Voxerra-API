namespace Voxerra_API.Controller.FriendAdd
{
    public class FriendRequestDecisionRequest
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public bool Decision { get; set; }
    }
}