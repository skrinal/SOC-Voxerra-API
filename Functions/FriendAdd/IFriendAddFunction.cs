
namespace Voxerra_API.Functions.FriendAdd
{
    public interface IFriendAddFunction
    {
        Task<IEnumerable<UserSearch>> SearchUsers(string query);
    }
}
