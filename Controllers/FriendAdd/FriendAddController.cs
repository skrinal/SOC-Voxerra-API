
namespace Voxerra_API.Controllers.FriendAdd
{
    [ApiController]
    [Route("[controller]")]
    public class FriendAddController(IFriendAddFunction friendAddFunction) : MvcController
    {
        public IFriendAddFunction _friendAddFunction = friendAddFunction;


        [HttpPost("Search")]
        public async Task<ActionResult> SearchUsers([FromBody] FriendSearchRequest request)
        {
            var response = new FriendSearchResponse
            {
                Users = await _friendAddFunction.SearchUsers(request.Search, request.IdOfUser)
            }; 

            return Ok(response);
        }

        [HttpPost("PublicProfile")]
        public async Task<ActionResult> PublicProfile([FromBody] int IdOfUser)
        {
            var result = await _friendAddFunction.PublicProfile(IdOfUser);
            
            return Ok(result);
        }
        
        
        [HttpPost("FriendRequest")]
        public async Task<ActionResult> FriendRequest([FromBody] FriendRequest request)
        {
            var result = await _friendAddFunction.FriendAddRequset(request.FromUserId, request.ToUserId);
            
            return Ok();
        }

        [HttpPost("FriendRequestList")]
        public async Task<ActionResult> FriendRequestList([FromBody] int IdOfUser)
        {
            var result = new FriendSearchResponse
            {
                Users = await _friendAddFunction.PendingRequestList(IdOfUser)
            }; 
            
            return Ok(result);
        }
        
        
        [HttpPost("FriendRequestDecision")]
        public async Task<ActionResult> FriendRequestDecision([FromBody] FriendDecisionRequest request)
        {
            var result = await _friendAddFunction.FriendRequestDecision
                (request.UserRequestFromId, request.UserRequestToId, request.Decision);
            
            return Ok(result);
        }

    }
}
