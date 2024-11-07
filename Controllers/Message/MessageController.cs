using Microsoft.AspNetCore.Mvc;
using Voxerra_API.Functions.Message;

namespace Voxerra_API.Controllers.Message
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class MessageController : Controller
    {
        IMessageFunction _messageFunction;
        IUserFunction _userFunction;

        public MessageController(IMessageFunction messageFunction, IUserFunction userFunction)
        {
            _messageFunction = messageFunction;
            _userFunction = userFunction;
        }

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
    }
}
