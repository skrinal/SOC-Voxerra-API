using Voxerra_API.Functions.User;

namespace Voxerra_API.Helpers
{
    public class UserOperator
    {
        IHttpContextAccessor _httpContext;
        public UserOperator(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public User? GetRequestUser()
        {
            if (_httpContext == null)
                return null;

            return _httpContext.HttpContext?.Items["User"] as User;
        }
    }
}
