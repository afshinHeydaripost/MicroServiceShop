using Admin.UI.Models;
using Helper.VieModels;
using Helper;
using Admin.UI.Class;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace Admin.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _config;
        private static string _productsServiceUrl;
        public ProductController(IConfiguration config)
        {
            _config = config;
            _productsServiceUrl = _config.GetValue<string>("ApiUrl:ProductsService").ToString();
        }
        #region Get   
        public async Task<IActionResult> Index()
        {
            var list = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductViewModel>>("api/Products/GetList", Request.GetCookiesValue("userToken"));

            return View();
        }
        #endregion

        #region Post
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetList()
        {
            var list =await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductViewModel>>("api/Products/GetList", Request.GetCookiesValue("userToken"));
            return Json(list);
        }
        #endregion
    }
}
