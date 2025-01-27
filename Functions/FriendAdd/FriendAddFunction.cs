
namespace Voxerra_API.Functions.FriendAdd
{
    public class FriendAddFunction(ChatAppContext chatAppContext) : IFriendAddFunction
    {
        ChatAppContext _chatAppContext = chatAppContext;

        public async Task<IEnumerable<UserSearch>> SearchUsers(string query)
        {
            var users = await _chatAppContext.Tblusers
                .Where(x => x.UserName.Contains(query))
                .OrderBy(x => x.UserName)
                .ThenBy(x => x.UserName.Length)
                //.ThenBy(x => x.UserName) // asi zle ??
                .Take(8)
                .Select(x => new UserSearch
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    AvatarSourceName = x.AvatarSourceName
                })
                .ToListAsync();
           
            if (users == null) users = new List<UserSearch>();
            
            return users;
        }

        public async Task<int> FriendAddRequset(int FromUser, int ToUser)
        {
            var entity = new TblPendingFriendRequest 
            { 
                FromUserId = FromUser, 
                ToUserId = ToUser,
            };

            _chatAppContext.Tblpendingfriendrequest.Add(entity);
            var result = await _chatAppContext.SaveChangesAsync();

            // treba poslat nieco druhemu klientovy pokial je online aby to videl hned

            return result;
        }
    }
}
