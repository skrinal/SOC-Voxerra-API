namespace Voxerra_API.Controllers.Authenticate;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Token { get; set; } = null!;
    public bool RequiresTwoFactorAuth { get; set; }
}