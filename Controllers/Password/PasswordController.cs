using Voxerra_API.Entities;
using Voxerra_API.Functions.Password;

namespace Voxerra_API.Controllers.Password
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController(IEmailFunction emailMessage, IPasswordFunction passwordFunction) : MvcController
    {
        private readonly IEmailFunction _emailMessage = emailMessage;
        private IPasswordFunction _passwordFunction = passwordFunction;

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] string email)
        {
            var reset = _passwordFunction.ResetPassword(email);

            if (reset.Result == true)
            {
                return Ok();
            }
            return BadRequest(new { message = "User email doesn't exist." });
        }

        [HttpPost("ResetPasswordConfirmation")]
        public async Task<ActionResult> ResetPasswordConfirmation([FromBody] PasswordResetConfirmationRequest request)
        {
            var changePass = _passwordFunction.ChangePasswordUsingCode(request.Email, request.Code, request.NewPassword);

            if (changePass.Result == true)
            {
                var details = new EmailDetails
                {
                    ToEmail = request.Email,
                    Subject = "Password Changed",
                    PasswordEmail = true
                };
                await _emailMessage.SendEmail(details);
                return Ok();
            }
            return BadRequest(new { message = "Failed to changed User password." });
        }

        /*TEST TEST
        [HttpPost("IbaEmail")]
        public async Task<ActionResult> IbaEmail([FromBody] string email)
        {
            var resetToken = _passwordFunction.GeneratePasswordResetToken(email);
            await _emailMessage.SendEmail(email, "Password Reset", resetToken);

            return Ok();
        }
        */
    }
}
