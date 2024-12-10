using Microsoft.AspNetCore.Mvc;
using Voxerra_API.Controllers.Message;
using Voxerra_API.Functions.Registration;

namespace Voxerra_API.Controllers.Registration
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        IUserRegistrationFunction _userRegistrationFunction;
        public RegistrationController(IUserRegistrationFunction userRegistrationFunction)
        {
            _userRegistrationFunction = userRegistrationFunction;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody] RegistrationRequest request)
        {


            var response = new RegistrationResponse
            {
                Successful = await _userRegistrationFunction.Registration(request.LoginId, request.Username, request.Password, request.Email)
            };


            if (response.Successful == false)
            {
                return StatusCode(500, new { message = "User registration failed due to server error" });
            }

            return Ok(response);
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
