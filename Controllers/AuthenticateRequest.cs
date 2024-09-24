namespace Voxerra_API.Controllers
{
    public class AuthenticateRequest
    {
        public string LoginId { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
