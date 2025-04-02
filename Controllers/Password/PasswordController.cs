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

        [HttpPost("RPCodeValidation")]
        public async Task<ActionResult> ResetPasswordCodeValidation([FromBody] RPCodeRequest request)
        {
            var changePass = _passwordFunction.ValidateCode(request.Code, request.Email);

            if (changePass.Result != Guid.Empty) return Ok(changePass.Result);
            
            return BadRequest(new { message = "Wrong code." });
        }
        
        [HttpPost("RPConfirm")]
        public async Task<ActionResult> ResetPasswordConfirm([FromBody] RPConfirmRequest request)
        {
            var result = _passwordFunction.ChangePasswordUsingCode(request.GuidAuth, request.NewPassword);

            if (result.Result.Success)
            {
                var details = new EmailDetails
                {
                    ToEmail = result.Result.Email,
                    PasswordEmail = true
                };
                await _emailMessage.SendEmail(details);
                return Ok();
            }
            return BadRequest(new { message = "Failed to changed User password." });
        }
        
        [HttpPost("SendNewCode")]
        public async Task<ActionResult> SendNewCodeToEmail([FromBody] string email)
        {
            var sendCode = _passwordFunction.SendCodeAgain(email);

            if (sendCode.Result)
            {
                return Ok();
            }
            return BadRequest(new { message = "Failed to send code." });
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
