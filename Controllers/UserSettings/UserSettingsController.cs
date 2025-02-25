
namespace Voxerra_API.Controllers.UserSettings
{
    [ApiController]
    [Route("[controller]")]
    public class UserSettingsController(ISettingFunction settingFunction, IEmailFunction emailFunction) :  BaseController
    {
        private ISettingFunction _settingFunction = settingFunction;
        private IEmailFunction _emailfunction = emailFunction;
    
        [HttpPost("ChangeUserName")]
        public async Task<ActionResult> ChangeUserName([FromBody] string NewUserName)
        {
            if (CurrentUser == null) return Unauthorized();

            var result = await _settingFunction.ChangeUserName(CurrentUser.Id, NewUserName);
            if (result) return Ok();
            
            return BadRequest();
        }
    
        [HttpPost("ReturnEmail")] 
        public async Task<ActionResult> ReturnEmail([FromBody] int userId)
        {
            var result = new EmailResponse
            {
                Email = await _settingFunction.ReturnEmail(userId)
            };
        
            return Ok(result);
        }
    
        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail([FromBody] UserEmailChangeRequest request)
        {
            try
            {
                if (CurrentUser == null) return Unauthorized();

                var emailPrompt = new EmailDetails
                {
                    ToEmail = CurrentUser.Email,

                };
                var email = _emailfunction.SendEmail(emailPrompt);
                return Ok();

                
            }
            catch (Exception )
            {
                return BadRequest();
            }
        }

        [HttpPost("ChangeEmailConfirm")]
        public async Task<ActionResult> ChangeEmailConfirm([FromBody] UserEmailChangeRequest request)
        {
            var result = await _settingFunction.ChangeEmail(request.UserId, request.NewEmail);
            if (result) return Ok();
            
            return BadRequest();
        }

        
    
        [HttpPost("ChangeBio")]
        public async Task<ActionResult> ChangeBio([FromBody] string NewBio)
        {
            if (CurrentUser == null) return Unauthorized();

            var result = await _settingFunction.ChangeBio(CurrentUser.Id, NewBio);
            if (result) return Ok();
            
            return BadRequest();
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] string NewPassword)
        {
            if (CurrentUser == null) return Unauthorized();

            var result = await _settingFunction.ChangePassword(CurrentUser.Id, NewPassword);
            if (result) return Ok();
            
            return BadRequest();
        }
        
        
        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser()
        {
            if (CurrentUser == null) return Unauthorized();

            var result = await _settingFunction.DeleteAccount(CurrentUser.Id);

            if (result) return Ok();
            
            return BadRequest();
        }

        [HttpPost("TwoAuth")]
        public async Task<ActionResult> TwoAuthChange([FromBody] bool Decision)
        {
            if (CurrentUser == null) return Unauthorized();
            
            var result = await _settingFunction.TwoAuthUpdate(CurrentUser.Id, Decision);
            if (result) return Ok();
            
            return BadRequest();
        }
    }
}
