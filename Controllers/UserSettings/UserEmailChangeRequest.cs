namespace Voxerra_API.Controllers.UserSettings;

public class UserEmailChangeRequest
{
    public int UserId { get; set; }
    public string NewEmail { get; set; } = null!;
}