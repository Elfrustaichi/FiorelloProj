using Microsoft.AspNetCore.Mvc;

namespace Fiorello.UI.Controllers
{
    public class FlowerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
