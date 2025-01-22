namespace Voxerra_API.Controllers.Password
{
    public class PasswordResetConfirmationRequest
    {
        public string NewPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Token { get; set; }

    }
}
