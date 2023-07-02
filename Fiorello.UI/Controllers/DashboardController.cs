using Fiorello.UI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
