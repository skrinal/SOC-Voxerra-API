namespace Voxerra_API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected User? CurrentUser
        {
            get
            {
                return HttpContext.Items["User"] as User;
            }
        }
    }
}