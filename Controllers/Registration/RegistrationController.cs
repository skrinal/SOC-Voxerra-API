using Microsoft.AspNetCore.Mvc;

namespace Voxerra_API.Controllers.Registration
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        public RegistrationController() 
        { 
        
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
