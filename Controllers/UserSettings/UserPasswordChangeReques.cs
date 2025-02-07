namespace Voxerra_API.Controllers.UserSettings;

public class UserPasswordChangeReques
{
    public int UserId { get; set; }
    public string NewPassword { get; set; } = null!;
}