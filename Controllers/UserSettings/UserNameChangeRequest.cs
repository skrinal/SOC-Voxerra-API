namespace Voxerra_API.Controllers.UserSettings;

public class UserNameChangeRequest
{
    public int UserId { get; set; }
    public string NewUserName { get; set; } = null!;
}