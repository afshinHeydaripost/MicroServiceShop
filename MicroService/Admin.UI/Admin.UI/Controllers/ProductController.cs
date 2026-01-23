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
            var list = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductViewModel>>($"api/Products/GetList", Request.GetCookiesValue("userToken"));
            var listProductCategory = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductCategoryViewModel>>($"api/ProductCategory/GetList?showAll={false}", Request.GetCookiesValue("userToken"));
            var listBrands = await _productsServiceUrl.SendAuthHeaderAndGetData<List<BrandViewModel>>($"api/Brand/GetList?showAll={false}", Request.GetCookiesValue("userToken"));
            var listProductColor = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductColorViewModel>>($"api/ProductColor/GetList?showAll={false}", Request.GetCookiesValue("userToken"));
            ProductModel item = new ProductModel()
            {
                Product = new ProductViewModel(),
                ProductsList = new List<ProductViewModel>(),
                ProductCategoryList = new List<ProductCategoryViewModel>(),
                BrandsList = new List<BrandViewModel>(),
                ProductColors = new List<ProductColorViewModel>(),
                ProductModels = new ProductModelViewMode(),
            };
            if (list is not null)
                item.ProductsList.AddRange(list);
            if (listProductCategory is not null)
                item.ProductCategoryList.AddRange(listProductCategory);
            if (listBrands is not null)
                item.BrandsList.AddRange(listBrands);
            if (listProductColor is not null)
                item.ProductColors.AddRange(listProductColor);
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
        public async Task<IActionResult> GetProductModel(int id)
        {
            var items = await _productsServiceUrl.SendAuthHeaderAndGetData<List<ProductModelViewMode>>($"api/ProductModels/GetList/{id}", Request.GetCookiesValue("userToken"));
            return PartialView("_ProductModels", items);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductModelItem(int id)
        {
            var item = await _productsServiceUrl.SendAuthHeaderAndGetData<ProductModelViewMode>($"api/ProductModels/{id}", Request.GetCookiesValue("userToken"));
            return Json(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = (_productsServiceUrl + $"api/Products/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            var res = (_productsServiceUrl + $"api/ProductModels/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> AddOrEditProduct([FromForm] ProductModel model)
        {
            if (model.Product.UploadedFile != null)
            {
                var resUpload = await model.Product.UploadedFile.FileToBase64(new List<FileSizeType>() {
                        new FileSizeType(){
                            Size=2000,
                            Type=FileType.Image
                        }
                });
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
        [HttpPost]
        public async Task<IActionResult> AddOrEditProductModel([FromForm] ProductModel model)
        {
            if (model.ProductModels.ColorId == null || model.ProductModels.ColorId == 0)
                return Json(GeneralResponse.Fail("رنگ را وارد کنید"));
            if (string.IsNullOrEmpty(model.ProductModels.strPrice))
                return Json(GeneralResponse.Fail("قیمت را وارد کنید"));
            if (model.ProductModels.ProductId == null || model.ProductModels.ProductId == 0)
            {
                return Json(GeneralResponse.Fail(Message.InvalidData));
            }
            model.ProductModels.Price = int.Parse(model.ProductModels.strPrice.Replace(",", ""));
            if (model.ProductModels.ProductModelId == null || model.ProductModels.ProductModelId == 0)
            {
                var res = (_productsServiceUrl + $"api/ProductModels/Create").SendAuthHeaderAndPostData<ProductModelViewMode, GeneralResponse>(model.ProductModels, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
            else
            {
                var res = (_productsServiceUrl + $"api/ProductModels/Update").SendAuthHeaderAndPostData<ProductModelViewMode, GeneralResponse>(model.ProductModels, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
        }

        #endregion
    }
}
