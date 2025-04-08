using System.Text.RegularExpressions;

namespace Voxerra_API.Functions.GroupChat;

public class GroupChatFunction(ChatAppContext chatAppContext) : IGroupChatFunction
{
    ChatAppContext _chatAppContext = chatAppContext;

    public async Task<bool> CreateGroup(string groupName, int creatorId)
    {
        try
        {
            var groupEntity = new TblGroups()
            {
                CreatedBy = creatorId,
                Name = groupName,
                CreatedAt = DateTime.Now,
                AvatarSourceName = "group.png"
            };
            
            _chatAppContext.Tblgroups.Add(groupEntity);
            await _chatAppContext.SaveChangesAsync();
            
            var groupMemberEntity = new TblGroupMembers()
            {
                GroupId = groupEntity.Id,  // Use the created group's Id
                UserId = creatorId,         // Add the creator as a member
                JoinedAt = DateTime.Now,  // Record the time they joined
            };
            
            _chatAppContext.Tblgroupmembers.Add(groupMemberEntity);
            await _chatAppContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        
        return true;
    }

    public async Task<bool> DeleteGroup(int groupId, int creatorId)
    {
        try
        {
            var group = await _chatAppContext.Tblgroups
                .FirstOrDefaultAsync(x => x.Id == groupId && x.CreatedBy == creatorId);
            
            if (group == null) return false;
            
            _chatAppContext.Tblgroups.Remove(group);
            await _chatAppContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<UserSearch>> WhoCanIAdd(int userId, string query)
    {
        try
        {
            var friendIds = await _chatAppContext.Tbluserfriends
                .Where(f => f.UserId == userId || f.FriendId == userId)
                .Select(f => f.UserId == userId ? f.FriendId : f.UserId)
                .ToListAsync();
            
            var friends = await _chatAppContext.Tblusers
                .Where(x => x.UserName.Contains(query) 
                            && x.Id != userId 
                            && friendIds.Contains(x.Id))
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
            
            return friends;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
        
        
    }
    
    public async Task<bool> AddToGroup(int groupId, int whoToAddId)
    {
        try
        {
            var entity = new TblGroupMembers()
            {
                GroupId = groupId,
                UserId = whoToAddId,
                JoinedAt = DateTime.Now
            };

            _chatAppContext.Tblgroupmembers.Add(entity);
            await _chatAppContext.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
    
    public async Task<IEnumerable<GroupDetails>> GroupList(int userId)
    {
        try
        {
            // 1. Get groups the user is part of
            var userGroupsQuery = _chatAppContext.Tblgroupmembers
                .Where(gm => gm.UserId == userId)
                .Join(
                    _chatAppContext.Tblgroups,
                    gm => gm.GroupId,
                    g => g.Id,
                    (gm, g) => g // Directly project the group
                );

            // 2. Get the latest message for each group (using a subquery)
            var query = 
                from groupEntity in userGroupsQuery
                // Use a subquery to fetch the latest message per group
                let latestMessage = _chatAppContext.Tblgroupmessages
                    .Where(m => m.GroupId == groupEntity.Id)
                    .OrderByDescending(m => m.SendDateTime)
                    .FirstOrDefault()
                // Calculate member count in a translatable way
                let membersCount = _chatAppContext.Tblgroupmembers
                    .Count(gm => gm.GroupId == groupEntity.Id)
                select new GroupDetails
                {
                    GroupId = groupEntity.Id,
                    GroupName = groupEntity.Name,
                    AvatarSourceName = groupEntity.AvatarSourceName,
                    CreatedBy = groupEntity.CreatedBy,
                    CreatedAt = groupEntity.CreatedAt,
                    MembersCount = membersCount,
                    LastMessage = latestMessage != null ? latestMessage.Content : null
                };

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<GroupDetails>();
            throw;
        }
    }

    public async Task<GroupMessagesResponse> GroupMessages(int groupId)
    {
        try
        {
            // Retrieve all messages for the group, ordered by send time
            var messages = await _chatAppContext.Tblgroupmessages
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.SendDateTime)
                .Select(m => new GroupMessages
                {
                    Content = m.Content,
                    SendDateTime = m.SendDateTime,
                    FromUserId = m.SenderId,
                    // Include other necessary properties from Tblgroupmessages
                })
                .ToListAsync();

            // Retrieve all members of the group and their user details
            var friendsInfo = await _chatAppContext.Tblgroupmembers
                .Where(gm => gm.GroupId == groupId)
                .Join(
                    _chatAppContext.Tblusers,
                    gm => gm.UserId,
                    u => u.Id,
                    (gm, u) => new User.User // Adjust namespace as necessary
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        AvatarSourceName = u.AvatarSourceName,
                        // Include other user properties as needed
                    })
                .ToListAsync();

            return new GroupMessagesResponse
            {
                FriendsInfo = friendsInfo,
                Messages = messages
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving group messages: {ex.Message}");
            return new GroupMessagesResponse
            {
                FriendsInfo = new List<User.User>(),
                Messages = new List<GroupMessages>()
            };
        }
    }



}