
namespace Voxerra_API.Functions.FriendAdd
{
    public interface IFriendAddFunction
    {
        Task<IEnumerable<UserSearch>> SearchUsers(string query, int userIdToExclude);
        Task<int> FriendAddRequset(int FromUser, int ToUser);
    }
}
