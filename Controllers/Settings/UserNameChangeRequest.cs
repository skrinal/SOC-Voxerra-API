namespace Voxerra_API.Controllers.Settings;

public class UserNameChangeRequest
{
    public int UserId { get; set; }
    public string NewUserName { get; set; }
}