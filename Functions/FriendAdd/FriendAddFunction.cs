namespace Voxerra_API.Functions.FriendAdd
{
    public class FriendAddFunction(ChatAppContext chatAppContext) : IFriendAddFunction
    {
        ChatAppContext _chatAppContext = chatAppContext;
        private readonly ILogger<FriendAddFunction> _logger;

        public async Task<IEnumerable<UserSearch>> SearchUsers(string query, int userIdToExclude)
        {
            var friendIds = await _chatAppContext.Tbluserfriends
                .Where(f => f.UserId == userIdToExclude || f.FriendId == userIdToExclude)
                .Select(f => f.UserId == userIdToExclude ? f.FriendId : f.UserId)
                .ToListAsync();
            
            var users = await _chatAppContext.Tblusers
                .Where(x => x.UserName.Contains(query) 
                            && x.Id != userIdToExclude 
                            && !friendIds.Contains(x.Id))
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

            return users ?? new List<UserSearch>();
        }

        public async Task<bool> FriendAddRequset(int FromUser, int ToUser)
        {
            bool alreadyExists = await _chatAppContext.Tblpendingfriendrequest
                .AnyAsync(x => x.FromUserId == FromUser && x.ToUserId == ToUser);

            if (alreadyExists) return true;

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
                return false;
            }

            // TODO: treba poslat nieco druhemu klientovy pokial je online aby to videl hned

            return true;
        }

        public async Task<UserPublicProfile> PublicProfile(int idOfUser, int currentUserId)
        {
            var userProfile = await _chatAppContext.Tblusers
                .Where(x => x.Id == idOfUser)
                .Select(user => new
                {
                    user.Bio,
                    user.IsOnline,
                    user.CreationDate,
                    FriendsCount = _chatAppContext.Tbluserfriends.Count(f => f.UserId == idOfUser),
                    IsFriend = _chatAppContext.Tbluserfriends
                        .Any(f => (f.UserId == idOfUser && f.FriendId == currentUserId) ||
                                  (f.UserId == currentUserId && f.FriendId == idOfUser)),
                    IsFriendRequest = _chatAppContext.Tblpendingfriendrequest
                        .Any(f => f.FromUserId == currentUserId && f.ToUserId == idOfUser),
                })
                .FirstOrDefaultAsync();

            if (userProfile == null)
            {
                return null;
            }

            var response = new UserPublicProfile
            {
                Bio = userProfile.Bio,
                FriendsCount = userProfile.FriendsCount,
                IsOnline = userProfile.IsOnline,
                CreationYear = userProfile.CreationDate.Year.ToString(),
                IsFriend = userProfile.IsFriend,
                IsFriendRequest = userProfile.IsFriendRequest
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
                    _logger.LogWarning(
                        $"No pending friend request found for FromUserId: {UserRequestFromId} and ToUserId: {UserRequestToId}");
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