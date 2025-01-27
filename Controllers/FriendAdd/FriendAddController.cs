
namespace Voxerra_API.Controllers.FriendAdd
{
    [ApiController]
    [Route("[controller]")]
    public class FriendAddController(IFriendAddFunction friendAddFunction) : MvcController
    {
        public IFriendAddFunction _friendAddFunction = friendAddFunction;


        [HttpPost("Search")]
        public async Task<ActionResult> SearchUsers([FromBody] String request)
        {
            var response = new FriendAddResponse
            {
                Users = await _friendAddFunction.SearchUsers(request)
            }; 

            return Ok(response);
        }


        [HttpPost("FriendRequest")]
        public async Task<ActionResult> FriendRequest([FromBody] FriendAddRequest request)
        {
            
            
            /*
            var pendingUser = await _chatAppContext.Tblpendingusers
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.VerificationCode == request.Code);

            if (pendingUser == null)
            {
                return BadRequest(new { message = "Invalid verification code or email." });
            }

            if (DateTime.UtcNow > pendingUser.ValidUntil)
            {
                return BadRequest(new { message = "Verification code has expired." });
            }

            var newUser = new TblUser
            {
                UserName = pendingUser.UserName,
                Email = pendingUser.Email,
                Password = pendingUser.Password,
                StoredSalt = pendingUser.StoredSalt,
                AvatarSourceName = "defaulticon.png"
            };

            _chatAppContext.Tblusers.Add(newUser);
            _chatAppContext.Tblpendingusers.Remove(pendingUser); 
            await _chatAppContext.SaveChangesAsync();
            */

            return Ok();
        }



    }
}
