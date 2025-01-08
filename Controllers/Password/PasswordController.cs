using Voxerra_API.Entities;
using Voxerra_API.Functions.Password;

namespace Voxerra_API.Controllers.Password
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController(IEmailFunction emailMessage, IPasswordFunction passwordFunction, ChatAppContext chatAppContext) : Controller
    {
        private readonly IEmailFunction _emailMessage = emailMessage;
        private readonly IPasswordFunction _passwordFunction = passwordFunction;
        private readonly ChatAppContext _chatAppContext = chatAppContext;

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] string email)
        {
            var validEmail = _chatAppContext.Tblusers.FirstOrDefault(x => x.Email == email);
            if (validEmail == null)
            {
                return NotFound("The email address is not registered.");
            }
            var resetToken = _passwordFunction.GeneratePasswordResetToken(email);
            
            var resetUrl = Url.Page("/Account/ChangePassword", "Account", new { token = resetToken }, Request.Scheme);

            await _emailMessage.SendEmail(email, "Password Reset", $"To reset your password, click the link: <a href='{resetUrl}'>Reset Password</a>");


            return Ok("Password reset email sent.");
        }
    }
}
