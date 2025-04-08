using Microsoft.AspNetCore.Mvc;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Controllers.Message
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessageController(IMessageFunction messageFunction, IUserFunction userFunction) : BaseController
    {
        IMessageFunction _messageFunction = messageFunction;
        IUserFunction  _userFunction= userFunction;
        
        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] MessageInitializeRequest request)
        {
            if (CurrentUser == null) return Unauthorized();
            
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
            if (CurrentUser == null) return Unauthorized();
            
            var response = new OldMessagesResponse
            {
                Messages = await _messageFunction.GetOldMessages(CurrentUser.Id, request.ToUserId, request.LastMessageId)
            };

            return Ok(response);
        }
    }
}
