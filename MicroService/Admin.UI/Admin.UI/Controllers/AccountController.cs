using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AccountController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
