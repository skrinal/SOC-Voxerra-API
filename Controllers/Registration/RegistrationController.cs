using Microsoft.AspNetCore.Mvc;
using Voxerra_API.Controllers.Message;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Registration;

namespace Voxerra_API.Controllers.Registration
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController(IUserRegistrationFunction userRegistrationFunction, ChatAppContext chatAppContext) : Controller
    {
        IUserRegistrationFunction _userRegistrationFunction = userRegistrationFunction;
        private readonly ChatAppContext _chatAppContext = chatAppContext;



        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody] RegistrationRequest request)
        {

            //var response = new RegistrationResponse
            //{
            //    Successful = await _userRegistrationFunction.Registration(request.Username, request.Password, request.Email)
            //};

            var response = await _userRegistrationFunction.Registration(request.Username, request.Password, request.Email);

            if (response == false)
            {
                return StatusCode(500, new { message = "User registration failed due to server error" });
            }

            return Ok(response);
        }


        [HttpPost("ConfirmRegistration")]
        public async Task<ActionResult> ConfirmRegistration([FromBody] ConfirmRegistrationRequest request)
        {
            var pendingUser = await _chatAppContext.Tblpendingusers
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.VerificationCode == request.Code);

            if (pendingUser == null)
            {
                return BadRequest(new { message = "Invalid verification code or email." });
            }

            if (pendingUser.CreatedAt.AddMinutes(10) < DateTime.UtcNow)
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

            return Ok(new { message = "Registration confirmed successfully." });
        }


        [HttpPost("IsEmailUnique")]
        public async Task<ActionResult> IsEmailUnique([FromBody] IsEmailUniqueRequest request)
        {

            var response = new IsEmailUniqueResponse
            {
                IsEmailUnique = await _userRegistrationFunction.IsEmailUnique(request.Email)
            };


            if (response.IsEmailUnique == false)
            {
                return BadRequest(new { message = "User email is not Unique" });
            }

            return Ok(response);
        }

        [HttpPost("IsUserNameUnique")]
        public async Task<ActionResult> IsUserNameUnique([FromBody] IsUserNameUniqueRequest request)
        {

            var response = new IsUserNameUniqueResponse
            {
                IsUserNameUnique = await _userRegistrationFunction.IsUserNameUnique(request.UserName)
            };

            if (response.IsUserNameUnique == false)
            {
                return BadRequest(new { message = "UserName is not Unique" });
            }

            return Ok(response);
        }



    }
}
