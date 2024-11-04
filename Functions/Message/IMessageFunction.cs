namespace Voxerra_API.Functions.Message
{
    public interface IMessageFunction
    {
        Task<IEnumerable <LastestMessage>> GetLastestMessage(int userId);
    }
}
