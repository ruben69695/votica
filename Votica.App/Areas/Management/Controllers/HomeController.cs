using Microsoft.AspNetCore.Mvc;

namespace Votica.App.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("management")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}