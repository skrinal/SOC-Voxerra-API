
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

            return Ok(result);
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
    
    }
}
