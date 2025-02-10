


namespace Voxerra_API.Controllers.Authenticate
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController(IUserFunction userFunction) : MvcController
    {
        private IUserFunction _userFunction = userFunction;

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _userFunction.Authenticate(request.UserName, request.Password);

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            if (response.RequiresTwoFactorAuth)
            {
                return StatusCode(222, response);
            }
            return Ok(response);
            
        }

        [HttpPost("TwoAuth")]
        public async Task<IActionResult> TwoAuthConfirm(TwoFactorAuthRequest request)
        {
            var response = await _userFunction.TwoFactorAuth(request.UserId, request.Code);

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(response);
            
        }


    }
}