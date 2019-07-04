using Microsoft.AspNetCore.Mvc;

namespace Votica.App.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("management/poll")]
    public class PollController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}