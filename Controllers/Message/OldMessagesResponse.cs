namespace Voxerra_API.Controllers.Message;

public class OldMessagesResponse
{
    public IEnumerable<Functions.Message.Message> Messages { get; set; } = null!;
}