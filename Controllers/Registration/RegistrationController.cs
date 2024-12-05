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
        [HttpPost("Register")]
        public async Task<ActionResult> Initialize([FromBody] RegistrationRequest request)
        {
            var response = await _userRegistrationFunction.Registration(request.Username, request.Password, request.Email);

            if (response == 0)
            {
                return BadRequest(new { message = "User registation failed" });
            }

            return Ok(response);
        }
    }
}
