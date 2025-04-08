namespace Voxerra_API.Functions.GroupChat;

public interface IGroupChatFunction
{
    Task<bool> CreateGroup(string groupName, int creatorId);
    Task<bool> DeleteGroup(int groupId, int creatorId);
    Task<IEnumerable<UserSearch>> WhoCanIAdd(int userId, string query);
    Task<bool> AddToGroup(int groupId, int whoToAddId);
    Task<IEnumerable<GroupDetails>> GroupList(int userId);
    Task<GroupMessagesResponse> GroupMessages(int groupId);
}