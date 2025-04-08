using System.Text.RegularExpressions;
using Voxerra_API.Functions.GroupChat;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Controllers.MessageCenter
{
    public class MessageCenterInitializeResponse
    {
        public User User { get; set; } = null!;
        public IEnumerable<User> UserFriends { get; set; } = null!;
        public IEnumerable<LastestMessage> LastestMessages { get; set; } = null!;
    }
}
