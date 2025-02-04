
namespace Voxerra_API.Controllers.Settings;

public class SettingController(ISettingFunction settingFunction) : MvcController
{
    private ISettingFunction _settingFunction = settingFunction;
    
    [HttpPost("ChangeUserName")]
    public async Task<ActionResult> ChangeUserName([FromBody] UserNameChangeRequest request)
    {
        var result = _settingFunction.ChangeUserName(request.UserId, request.NewUserName);

        return Ok(result);
    }
}