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
                Product=new ProductViewModel(),
                ProductsList=new List<ProductViewModel>(),
                ProductCategoryList=new List<ProductCategoryViewModel>(),
                BrandsList=new List<BrandViewModel>(),
            };
            if (list is not null)
                item.ProductsList.AddRange(list);
            if (listProductCategory is not null)
                item.ProductCategoryList.AddRange(listProductCategory);
            if (listBrands is not null)
                item.BrandsList.AddRange(listBrands);
            return View(item);
        }
        #endregion

        #region Post

        #endregion
    }
}
