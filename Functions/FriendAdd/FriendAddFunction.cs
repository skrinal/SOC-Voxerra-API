
namespace Voxerra_API.Functions.FriendAdd
{
    public class FriendAddFunction(ChatAppContext chatAppContext) : IFriendAddFunction
    {
        ChatAppContext _chatAppContext = chatAppContext;
        private readonly ILogger<FriendAddFunction> _logger;

        public async Task<IEnumerable<UserSearch>> SearchUsers(string query, int userIdToExclude)
        {
            var users = await _chatAppContext.Tblusers
                .Where(x => x.UserName.Contains(query) && x.Id != userIdToExclude)
                .OrderBy(x => x.UserName)
                .ThenBy(x => x.UserName.Length)
                .Take(6)
                .Select(x => new UserSearch
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    AvatarSourceName = x.AvatarSourceName,
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
                ToUserId = ToUser
            };
            try
            {
                _chatAppContext.Tblpendingfriendrequest.Add(entity);
                await _chatAppContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // treba poslat nieco druhemu klientovy pokial je online aby to videl hned

            return 1;
        }
        
        public async Task<UserPublicProfile> PublicProfile(int IdOfUser)
        {
            var user = await _chatAppContext.Tblusers.FirstOrDefaultAsync(x => x.Id == IdOfUser);
            var friendsCount = await _chatAppContext.Tbluserfriends.CountAsync(x => x.UserId == IdOfUser);
            
            var response = new UserPublicProfile
            {
                Bio = user.Bio,
                FriendsCount = friendsCount,
                IsOnline = user.IsOnline,
                CreationYear = user.CreationDate.Year.ToString()
            };
            
            return response;
        }

        public async Task<IEnumerable<UserSearch>> PendingRequestList(int ToUserId)
        {
            try
            {
                var FromUserId =
                    await _chatAppContext.Tblpendingfriendrequest
                        .Where(x => x.ToUserId == ToUserId)
                        .Select(x => x.FromUserId)
                        .ToListAsync();
            
                var users = await _chatAppContext.Tblusers
                    .Where(x => FromUserId.Contains(x.Id))
                    .Select(x => new UserSearch
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        AvatarSourceName = x.AvatarSourceName,
                    })
                    .ToListAsync();
                
                if (users == null) users = new List<UserSearch>();
                
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> FriendRequestDecision(int UserRequestFromId, int UserRequestToId, bool Decision)
        {
            try
            {
                var pendingRequest
                    = await _chatAppContext.Tblpendingfriendrequest
                    .FirstOrDefaultAsync(x => x.FromUserId == UserRequestFromId && x.ToUserId == UserRequestToId);
                
                if (pendingRequest == null)
                {
                    _logger.LogWarning($"No pending friend request found for FromUserId: {UserRequestFromId} and ToUserId: {UserRequestToId}");
                    return false;
                }
                
                _chatAppContext.Tblpendingfriendrequest.Remove(pendingRequest);
                
                if (Decision)
                {
                    var requests = new[]
                    {
                        new TblUserFriend { UserId = UserRequestToId, FriendId = UserRequestFromId, NickName = "" },
                        new TblUserFriend { UserId = UserRequestFromId, FriendId = UserRequestToId, NickName = "" }
                    };

                    _chatAppContext.Tbluserfriends.AddRange(requests);
                }

                await _chatAppContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the friend request decision.");
                return false;

            }
        }
    }
}
