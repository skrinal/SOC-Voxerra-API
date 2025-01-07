
using Voxerra_API.Entities;

namespace Voxerra_API.Functions.UserFriend
{
    public class UserFriendFunction : IUserFriendFunction
    {
        readonly ChatAppContext _chatAppContext;
        readonly IUserFunction _userFunction;
        public UserFriendFunction(ChatAppContext chatAppContext, IUserFunction userFunction)
        {
            _chatAppContext = chatAppContext;
            _userFunction = userFunction;
        }

        public async Task<IEnumerable<User.User>> GetListUserFriend(int userId)
        {
            var entities = await _chatAppContext.Tbluserfriends
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var result = entities.Select(x => _userFunction.GetUserById(x.FriendId));
            if (result == null) result = new List<User.User>();

            return result;
            
        }
    }
}
