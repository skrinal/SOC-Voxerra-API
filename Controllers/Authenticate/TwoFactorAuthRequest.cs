namespace Voxerra_API.Controllers.Authenticate;

public class TwoFactorAuthRequest
{
    public int UserId { get; set; }
    public int Code { get; set; }
}