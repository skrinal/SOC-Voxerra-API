
namespace Voxerra_API.Controllers.UserSettings
{
    [ApiController]
    [Route("[controller]")]
    public class UserSettingsController(ISettingFunction settingFunction) : MvcController
    {
        private ISettingFunction _settingFunction = settingFunction;
    
        [HttpPost("ChangeUserName")]
        public async Task<ActionResult> ChangeUserName([FromBody] UserNameChangeRequest request)
        {
            var result = await _settingFunction.ChangeUserName(request.UserId, request.NewUserName);
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
            var result = await _settingFunction.ChangeEmail(request.UserId, request.NewEmail);
            if (result) return Ok();
            
            return BadRequest();
        }
    
        [HttpPost("ChangeBio")]
        public async Task<ActionResult> ChangeBio([FromBody] UserBioChangeRequest request)
        {
            var result = await _settingFunction.ChangeBio(request.UserId, request.NewBio);
            if (result) return Ok();
            
            return BadRequest();
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UserPasswordChangeReques request)
        {
            var result = await _settingFunction.ChangePassword(request.UserId, request.NewPassword);
            if (result) return Ok();
            
            return BadRequest();
        }
        
        
        
        
        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromBody] int UserId)
        {
            var result = await _settingFunction.DeleteAccount(UserId);
            if (result) return Ok();
            
            return BadRequest();
        }

        [HttpPost("TwoAuth")]
        public async Task<ActionResult> TwoAuthChange([FromBody] UserTwoAuthRequest request)
        {
            var result = await _settingFunction.TwoAuthUpdate(request.UserId, request.Decision);
            if (result) return Ok();
            
            return BadRequest();
        }
    }
}
