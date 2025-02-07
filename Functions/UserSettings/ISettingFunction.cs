namespace Voxerra_API.Functions.Settings;

public interface ISettingFunction
{
    Task<bool> ChangeUserName(int userId, string newUserName);
    Task<string> ReturnEmail(int userId);
    Task<bool> ChangeEmail(int userId, string newEmail);
    Task<bool> ChangeBio(int userId, string newBio);
    Task<bool> ChangePassword(int userId, string newPassword);
    Task<bool> DeleteAccount(int userId);
    Task<bool> TwoAuthUpdate(int userId, bool decision);
}