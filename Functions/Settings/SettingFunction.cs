namespace Voxerra_API.Functions.Settings;

public class SettingFunction(ChatAppContext chatAppContext) : ISettingFunction
{
    private ChatAppContext _chatAppContext = chatAppContext;

    public async Task<bool> ChangeUserName(int userId, string newUserName)
    {
        try
        {
            var user = _chatAppContext.Tblusers.FirstOrDefault(x => x.Id == userId);

            user.UserName = newUserName;
            await _chatAppContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}