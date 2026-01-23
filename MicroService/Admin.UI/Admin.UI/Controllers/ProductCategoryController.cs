using Admin.UI.Class;
using Admin.UI.Models;
using Helper.VieModels;
using Helper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin.UI.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IConfiguration _config;
        private static string _productsServiceUrl;
        public ProductCategoryController(IConfiguration config)
        {
            _config = config;
            _productsServiceUrl = _config.GetValue<string>("ApiUrl:ProductsService").ToString();
        }
        #region Get

        public async Task<IActionResult> Index()
        {
            var listBrands = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductCategoryViewModel>>("api/ProductCategory/GetList", Request.GetCookiesValue("userToken"));
            ProductCategoryModel item = new ProductCategoryModel()
            {
                List = new List<ProductCategoryViewModel>(),
                Item = new ProductCategoryViewModel()
            };
            if (listBrands is not null)
                item.List.AddRange(listBrands);
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _productsServiceUrl.SendAuthHeaderAndGetData<ProductCategoryViewModel>($"api/ProductCategory/{id}", Request.GetCookiesValue("userToken"));
            return Json(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = (_productsServiceUrl + $"api/ProductCategory/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> AddOrEditProductCategory([FromForm] ProductCategoryModel model)
        {
            if (model.Item.UploadedFile != null)
            {
                var resUpload = await model.Item.UploadedFile.FileToBase64(new List<FileSizeType>() {
                        new FileSizeType(){
                            Size=2000,
                            Type=FileType.Image
                        }
                });
                if (!resUpload.isSuccess)
                    return Json(resUpload);
                model.Item.ImageUrl = resUpload.obj;
            }
            model.Item.UploadedFile = null;
            if (string.IsNullOrEmpty(model.Item.Title))
                return Json(GeneralResponse.Fail("عنوان را وارد کنید"));
            if (model.Item.ProductCategoryId == null || model.Item.ProductCategoryId == 0)
            {
                var res = (_productsServiceUrl + $"api/ProductCategory/Create").SendAuthHeaderAndPostData<ProductCategoryViewModel, GeneralResponse>(model.Item, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
            else
            {
                var res = (_productsServiceUrl + $"api/ProductCategory/Update").SendAuthHeaderAndPostData<ProductCategoryViewModel, GeneralResponse>(model.Item, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
        }

        #endregion
    }
}
