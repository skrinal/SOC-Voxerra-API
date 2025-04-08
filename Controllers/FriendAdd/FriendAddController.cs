
namespace Voxerra_API.Controllers.FriendAdd
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FriendAddController(IFriendAddFunction friendAddFunction) :  BaseController
    {
        public IFriendAddFunction _friendAddFunction = friendAddFunction;


        [HttpPost("Search")]
        public async Task<ActionResult> SearchUsers([FromBody] string searchQueary)
        {
            if (CurrentUser == null) return Unauthorized();
            
            var response = new FriendSearchResponse
            {
                Users = await _friendAddFunction.SearchUsers(searchQueary, CurrentUser.Id)
            }; 

            return Ok(response);
        }

        [HttpPost("PublicProfile")]
        public async Task<ActionResult> PublicProfile([FromBody] int idOfUser)
        {
            if (CurrentUser == null) return Unauthorized();
            
            var result = await _friendAddFunction.PublicProfile(idOfUser, CurrentUser.Id);
            
            return Ok(result);
        }
        
        
        [HttpPost("FriendRequest")]
        public async Task<ActionResult> FriendRequest([FromBody] int ToUserId)
        {
            if (CurrentUser == null) return Unauthorized();
            
            var result = await _friendAddFunction.FriendAddRequset(CurrentUser.Id, ToUserId);
            
            return Ok();
        }

        [HttpPost("FriendRequestList")]
        public async Task<ActionResult> FriendRequestList()
        {
            if (CurrentUser == null) return Unauthorized();
            
            var result = new FriendSearchResponse
            {
                Users = await _friendAddFunction.PendingRequestList(CurrentUser.Id)
            }; 
            
            return Ok(result);
        }
        
        
        [HttpPost("FriendRequestDecision")]
        public async Task<ActionResult> FriendRequestDecision([FromBody] FriendDecisionRequest request)
        {
            if (CurrentUser == null) return Unauthorized();
            
            var result = await _friendAddFunction.FriendRequestDecision
                (request.UserRequestFromId, CurrentUser.Id, request.Decision);
            
            return Ok(result);
        }

    }
}
