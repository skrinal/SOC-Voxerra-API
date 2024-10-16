﻿namespace Voxerra_API.Controllers.Authenticate
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
            var response = _userFunction.Authenticate(request.LoginId, request.Password);

            if (response == null)
            {
                _logger.LogWarning("Authentication failed for user: {LoginId}", request.LoginId);
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(response);
        }
    }
}