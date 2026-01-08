using Admin.UI.Class;
using Admin.UI.Models;
using Helper.VieModels;
using Helper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin.UI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IConfiguration _config;
        private static string _productsServiceUrl;
        public DiscountController(IConfiguration config)
        {
            _config = config;
            _productsServiceUrl = _config.GetValue<string>("ApiUrl:ProductsService").ToString();
        }
        #region Get

        public async Task<IActionResult> Index()
        {
            var list = await _productsServiceUrl.SendAuthHeaderAndGetData<List<DiscountViewModel>>("api/Discount/GetList", Request.GetCookiesValue("userToken"));
            var listProductCategory = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductCategoryViewModel>>($"api/ProductCategory/GetList?showAll={false}", Request.GetCookiesValue("userToken"));
            var listBrands = await _productsServiceUrl.SendAuthHeaderAndGetData<List<BrandViewModel>>($"api/Brand/GetList?showAll={false}", Request.GetCookiesValue("userToken"));
            DiscountModel item = new DiscountModel()
            {
                List = new List<DiscountViewModel>(),
                Item=new DiscountViewModel(),
                ProductCategoryList = new List<ProductCategoryViewModel>(),
                BrandsList = new List<BrandViewModel>(),

            };
            if (list is not null)
                item.List.AddRange(list);
            if (listProductCategory is not null)
                item.ProductCategoryList.AddRange(listProductCategory);
            if (listBrands is not null)
                item.BrandsList.AddRange(listBrands);
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _productsServiceUrl.SendAuthHeaderAndGetData<DiscountViewModel>($"api/Discount/{id}", Request.GetCookiesValue("userToken"));
            return Json(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = (_productsServiceUrl + $"api/Discount/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> AddOrEditDiscount([FromForm] DiscountModel model)
        {
            if (model.Item.DiscountId == null || model.Item.DiscountId == 0)
            {
                var res = (_productsServiceUrl + $"api/Discount/Create").SendAuthHeaderAndPostData<DiscountViewModel, GeneralResponse>(model.Item, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
            else
            {
                var res = (_productsServiceUrl + $"api/Discount/Update").SendAuthHeaderAndPostData<DiscountViewModel, GeneralResponse>(model.Item, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
        }

        #endregion
    }
}
