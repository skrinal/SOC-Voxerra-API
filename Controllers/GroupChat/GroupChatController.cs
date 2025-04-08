using Voxerra_API.Functions.GroupChat;

namespace Voxerra_API.Controllers.GroupChat
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GroupChatController(IGroupChatFunction groupChatFunction) : BaseController
    {
        public IGroupChatFunction _groupChatFunction = groupChatFunction;

        [HttpPost("CreateGroup")]
        public async Task<ActionResult> CreateGroupChat([FromBody] string groupName)
        {
            if (CurrentUser == null) return Unauthorized();

            var response = _groupChatFunction.CreateGroup(groupName, CurrentUser.Id);

            if (!response.Result) return BadRequest();

            return Ok();
        }
        
        [HttpPost("GetGroupLists")]
        public async Task<ActionResult> GetGroups()
        {
            if (CurrentUser == null) return Unauthorized();
            
            var response = new GroupChatCenterResponse
            {
                DetailsOfGroups = await _groupChatFunction.GroupList(CurrentUser.Id)
            };
            
            return Ok(response);
        }
        
        [HttpPost("GetGroupMessages")]
        public async Task<ActionResult> GetMessages([FromBody] int groupId)
        {
            if (CurrentUser == null) return Unauthorized();

            var response = await _groupChatFunction.GroupMessages(groupId);
                
            return Ok(response);
        }
    }
}

