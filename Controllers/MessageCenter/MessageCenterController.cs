
namespace Voxerra_API.Controllers.MessageCenter
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessageCenterController : BaseController
    {
        IUserFunction _userFunction;
        IUserFriendFunction _userFriendFunction;
        IMessageFunction _messageFunction;

        public MessageCenterController(IUserFunction userFunction, IUserFriendFunction userFriendFunction, IMessageFunction messageFunction)
        {
            _userFunction = userFunction;
            _userFriendFunction = userFriendFunction;
            _messageFunction = messageFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] int idOfUser)
        {
            
            var response = new MessageCenterInitializeResponse
            {
                User = _userFunction.GetUserById(idOfUser),
                UserFriends = await _userFriendFunction.GetListUserFriend(idOfUser),
                LastestMessages = await _messageFunction.GetLatestMessage(idOfUser),
            };

            return Ok(response);
        }
    }
}
