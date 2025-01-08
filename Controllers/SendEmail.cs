using Microsoft.AspNetCore.Mvc;
using Voxerra_API.Controllers.Registration;
using Voxerra_API.Functions.Registration;

namespace Voxerra_API.Helpers
{
    [ApiController]
    [Route("[controller]")]
    public class SendEmail : Controller
    {
        EmailMessage _emailMessage;
        public SendEmail(EmailMessage emailMessage)
        {
            _emailMessage = emailMessage;
        }


        [HttpPost]
        public async Task<ActionResult> SendEmailToUser([FromBody] EmailRequest request)
        {

            var status = await _emailMessage.SendEmailAsync("richard.kamenistak@gmail.com", "Verification code", "123456");

            if (status == false)
            {
                return StatusCode(500, new { message = "Email failed to be send" });
            }

            return Ok(status);
        }
    }
}
