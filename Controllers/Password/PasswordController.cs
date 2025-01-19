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
            if (validEmail != null)
            {
                var resetToken = _passwordFunction.GeneratePasswordResetToken(email);

                //await _emailMessage.SendEmail(email, "Password Reset", $"To reset your password, click the link: <a href='{resetUrl}'>Reset Password</a>");

                //await _emailMessage.SendEmail(email, "Password Reset", $"To reset your password, use this code: {resetToken}");
            }
            return Ok();
        }

        [HttpPost("IbaEmail")]
        public async Task<ActionResult> IbaEmail([FromBody] string email)
        {
            var resetToken = _passwordFunction.GeneratePasswordResetToken(email);
            await _emailMessage.SendEmail(email, "Password Reset", resetToken);

            return Ok();
        }
    }
}
