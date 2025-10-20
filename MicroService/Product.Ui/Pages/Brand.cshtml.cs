using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Ui.Class;

namespace Product.Ui.Pages
{
    public class BrandModel : CustomePageModel
    {
        private readonly ApiService _apiService;
        public List<BrandViewModel> brnds { get; set; }

        [BindProperty]
        public BrandViewModel brand { get; set; }
        public BrandModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task OnGet()
        {
            brnds = await  _apiService.GetDataAsync<List<BrandViewModel>>(ProductApiUrl, "api/Brand/GetList");
        }

        public async Task<IActionResult> OnGetById(int id)
        {
            return new JsonResult(await _apiService.GetDataAsync<BrandViewModel>(ProductApiUrl, $"api/Brand/{id}"));
        }
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostAddAsync()
        {
            var res = GeneralResponse.Fail();
            if (!ModelState.IsValid)
                res.Message = Message.InvalidData;
            else
            {
                if (brand.UploadedFile != null)
                {
                    var fileName = "/images/Brand/" + Guid.NewGuid().ToString().Replace("-", "");
                    List<FileSizeType> fileSizeType = new List<FileSizeType>() {
                    new FileSizeType(){
                        Size=300,
                    },
                };
                    var resfile = await Uploder.UploadFile(brand.UploadedFile, fileName, fileSizeType);
                    if (resfile.isSuccess)
                        brand.Logo = resfile.Url;
                    else
                    {
                        res.Message = "تصویر لوگو <br/> " + resfile.ErrorMessage;
                        return new JsonResult(res);
                    }
                }
                res = await _apiService.PostDataAsync<BrandViewModel, GeneralResponse>(ProductApiUrl, "api/Brand/GetList",brand);
            }
            return new JsonResult(res);
        }
    }
}
