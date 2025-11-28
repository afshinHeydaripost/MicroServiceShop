using Admin.UI.Class;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
