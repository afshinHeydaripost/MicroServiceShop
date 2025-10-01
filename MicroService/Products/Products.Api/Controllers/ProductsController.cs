using Microsoft.AspNetCore.Mvc;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
