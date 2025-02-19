using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Voxerra_API.Helpers
{
    public class JwtMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context, IUserFunction userFunction, ILogger<JwtMiddleware> logger)
        {
            logger.LogInformation("Incoming request: {Path}", context.Request.Path);

            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(' ').Last();
            if (token == null)
                token = context.Request.Headers["ChatHubBearer"].FirstOrDefault()?.Split(' ').Last();

            if (token != null)
                AttachUserToContext(context, userFunction, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserFunction userFunction, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("rweofkwurtihonmoiurwhbnrtwrgwrgjge");
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                
                context.Items["User"] = userFunction.GetUserById(userId);
            }
            catch
            {

            }
        }
    }
}

/*
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Voxerra_API.Helpers
{
    public class JwtMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context, IUserFunction userFunction, ILogger<JwtMiddleware> logger)
        {
            logger.LogInformation("➡️ Incoming request: {Path}", context.Request.Path);

            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(' ').Last();
            if (token == null)
                token = context.Request.Headers["ChatHubBearer"].FirstOrDefault()?.Split(' ').Last();

            if (token != null)
            {
                logger.LogInformation("🔑 Extracted Token: {Token}", token);
                AttachUserToContext(context, userFunction, token, logger);
            }
            else
            {
                logger.LogWarning("⚠️ No JWT token found in request headers.");
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserFunction userFunction, string token, ILogger<JwtMiddleware> logger)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("rweofkwurtihonmoiurwhbnrtwrgwrgjge");
                
                logger.LogInformation("🔍 Validating JWT token...");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "id");

                if (userIdClaim == null)
                {
                    logger.LogWarning("⚠️ No 'id' claim found in token.");
                    return;
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    logger.LogWarning("⚠️ Invalid user ID in JWT token: {UserIdClaim}", userIdClaim.Value);
                    return;
                }

                logger.LogInformation("✅ Extracted UserId: {UserId}", userId);
                
                var user = userFunction.GetUserById(userId);
                if (user == null)
                {
                    logger.LogWarning("⚠️ User with ID {UserId} not found.", userId);
                }
                else
                {
                    logger.LogInformation("✅ User {UserId} found and attached to context.", userId);
                }

                context.Items["User"] = user;
            }
            catch (Exception ex)
            {
                logger.LogError("❌ JWT validation failed: {Message}", ex.Message);
            }
        }
    }
}
*/
