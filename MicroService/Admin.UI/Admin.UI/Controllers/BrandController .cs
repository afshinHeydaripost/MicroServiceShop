using Admin.UI.Class;
using Admin.UI.Models;
using Helper.VieModels;
using Helper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Admin.UI.Controllers
{
	[Authorize(Roles = Roles.Admin)]
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
            var item = await _productsServiceUrl.SendAuthHeaderAndGetData<BrandViewModel>($"api/Brand/{id}", Request.GetCookiesValue("userToken"));
            return Json(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = (_productsServiceUrl + $"api/Brand/delete/{id}").SendAuthHeaderAndPostData<int, GeneralResponse>(id, Request.GetCookiesValue("userToken"));
            return Json(res);
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> AddOrEditBrand([FromForm] BrandModel model)
        {
            if (model.Brand.UploadedFile != null)
            {
                var resUpload = await model.Brand.UploadedFile.FileToBase64(new List<FileSizeType>() {
                        new FileSizeType(){
                            Size=2000,
                            Type=FileType.Image
                        }
                });
                if (!resUpload.isSuccess)
                    return Json(resUpload);
                model.Brand.Logo = resUpload.obj;
            }
            model.Brand.UploadedFile = null;
            if (string.IsNullOrEmpty(model.Brand.Title))
                return Json(GeneralResponse.Fail("عنوان را وارد کنید"));
            if (model.Brand.BrandId == null || model.Brand.BrandId == 0)
            {
                var res = (_productsServiceUrl + $"api/Brand/Create").SendAuthHeaderAndPostData<BrandViewModel, GeneralResponse>(model.Brand, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
            else
            {
                var res = (_productsServiceUrl + $"api/Brand/Update").SendAuthHeaderAndPostData<BrandViewModel, GeneralResponse>(model.Brand, Request.GetCookiesValue("userToken"));
                return Json(res);
            }
        }

        #endregion
    }
}
