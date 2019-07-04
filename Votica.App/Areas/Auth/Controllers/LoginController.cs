using Microsoft.AspNetCore.Mvc;

namespace Votica.App.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("auth/login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}