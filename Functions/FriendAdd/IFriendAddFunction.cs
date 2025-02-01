
namespace Voxerra_API.Functions.FriendAdd
{
    public interface IFriendAddFunction
    {
        Task<IEnumerable<UserSearch>> SearchUsers(string query, int userIdToExclude);
        Task<int> FriendAddRequset(int FromUser, int ToUser);
        Task<UserPublicProfile> PublicProfile(int IdOfUser);

        Task<IEnumerable<UserSearch>> PendingRequestList(int ToUserId);
        Task<bool> FriendRequestDecision(int UserRequestFromId, int UserRequestToId, bool Decision);
    }
}
