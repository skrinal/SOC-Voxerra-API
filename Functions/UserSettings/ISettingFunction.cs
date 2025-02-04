namespace Voxerra_API.Functions.Settings;

public interface ISettingFunction
{
    Task<bool> ChangeUserName(int userId, string newUserName);
    Task<string> ReturnEmail(int userId);
    Task<bool> ChangeEmail(int userId, string newEmail);
}