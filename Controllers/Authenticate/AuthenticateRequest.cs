using Microsoft.Build.Framework;

namespace Voxerra_API.Controllers.Authenticate
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
