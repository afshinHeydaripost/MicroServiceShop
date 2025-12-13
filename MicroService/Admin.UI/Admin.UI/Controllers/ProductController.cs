using Admin.UI.Models;
using Helper.VieModels;
using Helper;
using Admin.UI.Class;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

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
            var listProductCategory = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductCategoryViewModel>>("api/ProductCategory/GetList", Request.GetCookiesValue("userToken"));
            var listBrands = await _productsServiceUrl.SendAuthHeaderAndGetData<List<BrandViewModel>>("api/Brand/GetList", Request.GetCookiesValue("userToken"));
            ProductModel item = new ProductModel()
            {
                Product = new ProductViewModel(),
                ProductsList = new List<ProductViewModel>(),
                ProductCategoryList = new List<ProductCategoryViewModel>(),
                BrandsList = new List<BrandViewModel>(),
            };
            if (list is not null)
                item.ProductsList.AddRange(list);
            if (listProductCategory is not null)
                item.ProductCategoryList.AddRange(listProductCategory);
            if (listBrands is not null)
                item.BrandsList.AddRange(listBrands);
            return View(item);
        }
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _productsServiceUrl.SendAuthHeaderAndGetData<ProductViewModel>($"api/Products/{id}", Request.GetCookiesValue("userToken"));
            return Json(item);
        }
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCode()
        {
            var code = await _productsServiceUrl.SendAuthHeaderAndGetData<string>($"api/Products/GetCode", Request.GetCookiesValue("userToken"));
            return Json(code);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = (_productsServiceUrl + $"api/Products/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> AddOrEditProduct([FromForm] ProductModel model)
        {
            if (model.Product.UploadedFile != null)
            {
                var resUpload = await model.Product.UploadedFile.UploadFile(Guid.NewGuid().ToString().Replace("-", ""), new List<FileSizeType>() {
                        new FileSizeType(){
                            Size=2000,
                            Type=FileType.Image
                        }
                }, _config.GetValue<string>("DomainName").ToString());
                if (!resUpload.isSuccess)
                    return Json(resUpload);
                model.Product.Picture = resUpload.obj;
            }
            model.Product.UploadedFile = null;
            if (model.Product.BrandId == null || model.Product.BrandId == 0)
                return Json(GeneralResponse.Fail("برند راوارد کنید"));
            if (model.Product.CategoryId == null || model.Product.CategoryId == 0)
                return Json(GeneralResponse.Fail("گروه کالا راوارد کنید"));
            if (model.Product.ProductId == null || model.Product.ProductId == 0)
            {
                var res = (_productsServiceUrl + $"api/Products/Create").SendAuthHeaderAndPostData<ProductViewModel, GeneralResponse>(model.Product, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
            else
            {
                var res = (_productsServiceUrl + $"api/Products/Update").SendAuthHeaderAndPostData<ProductViewModel, GeneralResponse>(model.Product, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
        }

        #endregion
    }
}
