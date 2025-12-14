using Admin.UI.Class;
using Admin.UI.Models;
using Helper.VieModels;
using Helper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin.UI.Controllers
{
    public class BrandController : Controller
    {
        private readonly IConfiguration _config;
        private static string _productsServiceUrl;
        public BrandController(IConfiguration config)
        {
            _config = config;
            _productsServiceUrl = _config.GetValue<string>("ApiUrl:ProductsService").ToString();
        }
        #region Get

        public async Task<IActionResult> Index()
        {
            var listBrands = await _productsServiceUrl.SendAuthHeaderAndGetData<List<BrandViewModel>>("api/Brand/GetList", Request.GetCookiesValue("userToken"));
            BrandModel item = new BrandModel()
            {
                BrandsList = new List<BrandViewModel>(),
                Brand=new BrandViewModel()
            };
            if (listBrands is not null)
                item.BrandsList.AddRange(listBrands);
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _productsServiceUrl.SendAuthHeaderAndGetData<ProductViewModel>($"api/Brand/{id}", Request.GetCookiesValue("userToken"));
            return Json(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = (_productsServiceUrl + $"api/Brand/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        #endregion
    }
}
