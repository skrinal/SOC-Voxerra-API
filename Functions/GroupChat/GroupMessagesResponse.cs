namespace Voxerra_API.Functions.GroupChat;

public class GroupMessagesResponse
{
    public IEnumerable<User.User> FriendsInfo { get; set; }
    public IEnumerable<GroupMessages>? Messages { get; set; }
}