using Microsoft.AspNetCore.Mvc;

namespace Fiorello.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
