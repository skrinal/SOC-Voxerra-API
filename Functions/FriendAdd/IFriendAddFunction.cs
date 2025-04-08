
namespace Voxerra_API.Functions.FriendAdd
{
    public interface IFriendAddFunction
    {
        Task<IEnumerable<UserSearch>> SearchUsers(string query, int userIdToExclude);
        Task<bool> FriendAddRequset(int FromUser, int ToUser);
        Task<UserPublicProfile> PublicProfile(int idOfUser, int currentUserId);
        Task<IEnumerable<UserSearch>> PendingRequestList(int ToUserId);
        Task<bool> FriendRequestDecision(int UserRequestFromId, int UserRequestToId, bool Decision);
    }
}
