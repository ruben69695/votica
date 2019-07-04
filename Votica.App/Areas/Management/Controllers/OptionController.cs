using Microsoft.AspNetCore.Mvc;

namespace Votica.App.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("management/option")]
    public class OptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}