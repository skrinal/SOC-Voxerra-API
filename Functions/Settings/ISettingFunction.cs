namespace Voxerra_API.Functions.Settings;

public interface ISettingFunction
{
    Task<bool> ChangeUserName(int userId, string newUserName);
}