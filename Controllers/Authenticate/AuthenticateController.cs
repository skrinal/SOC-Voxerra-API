using Microsoft.Extensions.Logging;

namespace Voxerra_API.Controllers.Authenticate
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IUserFunction _userFunction;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IUserFunction userFunction, ILogger<AuthenticateController> logger)
        {
            _userFunction = userFunction;
            _logger = logger;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userFunction.Authenticate(request.UserName, request.Password);

            if (response == null)
            {
                _logger.LogWarning("Authentication failed for user: {UserName}", request.UserName);
                return Unauthorized(new { message = "Invalid credentials" });
            }

            _logger.LogInformation($"User {request.UserName} has been logged in");
            return Ok(response);
            
        }
    }
}