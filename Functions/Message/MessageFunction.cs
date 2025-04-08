using Voxerra_API.Entities;

namespace Voxerra_API.Functions.Message
{
    public class MessageFunction : IMessageFunction
    {
        readonly ChatAppContext _chatAppContext;
        readonly IUserFunction _userFunction;
        public MessageFunction(ChatAppContext chatAppContext, IUserFunction userFunction)
        {
            _chatAppContext = chatAppContext;
            _userFunction = userFunction;
        }

        public async Task<int> AddMessage(int fromUserId, int toUserId, string message)
        {
            var entity = new TblMessage 
            { 
                FromUserId = fromUserId, 
                ToUserId = toUserId,
                Content = message,
                SendDateTime = DateTime.Now,
                IsRead = false
            };

            _chatAppContext.Tblmessages.Add(entity);
            var result = await _chatAppContext.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<LastestMessage>> GetLatestMessage(int userId)
        {
            var result = new List<LastestMessage>();

            var userFriends = await _chatAppContext.Tbluserfriends
                .Where(x => x.UserId == userId)
                .ToListAsync();

            foreach (var userFriend in userFriends)
            {
                var lastMessage = await _chatAppContext.Tblmessages
                    .Where(x => (x.FromUserId == userId && x.ToUserId == userFriend.FriendId)
                             || (x.FromUserId == userFriend.FriendId && x.ToUserId == userId))
                    .OrderByDescending(x => x.SendDateTime)
                    .Take(10)
                    .FirstOrDefaultAsync();

                if (lastMessage != null)
                {
                    result.Add(new LastestMessage
                    {
                        UserId = userId,
                        Content = lastMessage.Content,
                        UserFriendInfo = _userFunction.GetUserById(userFriend.FriendId),
                        Id = lastMessage.Id,
                        IsRead = lastMessage.IsRead,
                        SendDateTime = lastMessage.SendDateTime,
                    });
                }
                else
                {
                    // If there is no message, add a default LastestMessage with an empty content
                    result.Add(new LastestMessage
                    {
                        UserId = userId,
                        Content = "",  // or "No messages yet" if you want a more descriptive text
                        UserFriendInfo = _userFunction.GetUserById(userFriend.FriendId),
                        //Id = 0, // or some other default value indicating no message ID
                        IsRead = false, // or true, depending on your requirements
                        SendDateTime = DateTime.MinValue // or another default date
                    });
                }
            }
            return result;
        }

        public async Task<IEnumerable<Message>> GetMessages(int fromUserId, int toUserId)
        {
            var entities = await _chatAppContext.Tblmessages
                .Where(x => (x.FromUserId == fromUserId && x.ToUserId == toUserId)
                || (x.FromUserId == toUserId && x.ToUserId == fromUserId))
                .OrderByDescending(x => x.SendDateTime)
                .Take(10)
                .ToListAsync();

            return entities
                .OrderBy(x => x.SendDateTime)
                .Select(x => new Message
            {
                Id = x.Id,
                Content = x.Content,
                FromUserId = x.FromUserId,
                ToUserId = x.ToUserId,
                SendDateTime = x.SendDateTime,
                IsRead = x.IsRead,
            });
        }
        
        public async Task<IEnumerable<Message>> GetOldMessages(int fromUserId, int toUserId, int lastMessageId)
        {
            var entities = await _chatAppContext.Tblmessages
                .Where(x => ((x.FromUserId == fromUserId && x.ToUserId == toUserId)
                            || (x.FromUserId == toUserId && x.ToUserId == fromUserId))
                            && x.Id < lastMessageId)
                .OrderByDescending(x => x.SendDateTime)
                .Take(10)
                .ToListAsync();



            return entities.Select(x => new Message
            {
                Id = x.Id,
                Content = x.Content,
                FromUserId = x.FromUserId,
                ToUserId = x.ToUserId,
                SendDateTime = x.SendDateTime,
                IsRead = x.IsRead,
            }).Reverse();
        }
    }
}
