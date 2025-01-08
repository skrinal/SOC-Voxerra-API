
namespace Voxerra_API.Controllers.Password
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PasswordController(IUserFunction userFunction, EmailMessage emailMessage) : Controller
    {
        IUserFunction _userFunction = userFunction;
        EmailMessage _emailMessage = emailMessage;

        [HttpPost("PasswordReset")]
        public async Task<ActionResult> PasswordReset([FromBody] string email)
        {
            var response = new MessageCenterInitializeResponse
            {
                User = _userFunction.GetUserById(userId),
                UserFriends = await _userFriendFunction.GetListUserFriend(userId),
                LastestMessages = await _messageFunction.GetLatestMessage(userId)
            };

            return Ok(response);
        }
    }
}
