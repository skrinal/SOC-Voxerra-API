namespace Voxerra_API.Functions.Message
{
    public interface IMessageFunction
    {
        Task<IEnumerable<LastestMessage>> GetLatestMessage(int userId);

        Task<IEnumerable<Message>> GetMessages(int fromUserId, int toUserId);

        Task<int> AddMessage(int fromUserId, int toUserId, string message);

        Task<IEnumerable<Message>> GetOldMessages(int fromUserId, int toUserId, int lastMessageId);
    }
}
