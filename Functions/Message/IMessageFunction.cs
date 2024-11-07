namespace Voxerra_API.Functions.Message
{
    public interface IMessageFunction
    {
        Task<IEnumerable<LastestMessage>> GetLatestMessage(int userId);

        Task<IEnumerable<Message>> GetMessages(int fromUserId, int toUserId);
    }
}
