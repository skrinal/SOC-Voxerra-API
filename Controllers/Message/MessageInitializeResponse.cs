namespace Voxerra_API.Controllers.Message
{
    public class MessageInitializeResponse
    {
        public User FriendInfo { get; set; } = null!;
        public IEnumerable<Functions.Message.Message> Messages { get; set; } = null!;
    }
}
