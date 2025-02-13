﻿
namespace Voxerra_API.Controllers.MessageCenter
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessageCenterController : MvcController
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
        public async Task<ActionResult> Initialize([FromBody] int userId)
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
