using Microsoft.AspNetCore.Mvc;

namespace Votica.App.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("management/question")]
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}