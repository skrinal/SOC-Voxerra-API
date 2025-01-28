
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


        [HttpPost("FriendRequest")]
        public async Task<ActionResult> FriendRequest([FromBody] FriendAddRequest request)
        {
            
            var result = _friendAddFunction.FriendAddRequset(request.FromUserId, request.ToUserId);
            

            return Ok();
        }

        [HttpPost("FriendRequestDecision")]
        public async Task<ActionResult> FriendRequestDecision([FromBody] FriendAddRequest request)
        {
            
        
            
    
            return Ok();
        }

    }
}
