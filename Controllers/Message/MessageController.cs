using Microsoft.AspNetCore.Mvc;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Controllers.Message
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessageController(IMessageFunction messageFunction, IUserFunction userFunction) : MvcController
    {
        IMessageFunction _messageFunction = messageFunction;
        IUserFunction  _userFunction= userFunction;
        
        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] MessageInitializeRequest request)
        {
            var response = new MessageInitializeResponse
            {
                FriendInfo = _userFunction.GetUserById(request.ToUserId),
                Messages = await _messageFunction.GetMessages(request.FromUserId, request.ToUserId),
            };

            return Ok(response);
        }
        
        [HttpPost("OldMessages")]
        public async Task<ActionResult> GetOldMessages([FromBody] OldMessagesRequest request)
        {
            var response = new OldMessagesResponse
            {
                Messages = await _messageFunction.GetOldMessages(request.FromUserId, request.ToUserId, request.LastMessageId)
            };

            return Ok(response);
        }
    }
}
