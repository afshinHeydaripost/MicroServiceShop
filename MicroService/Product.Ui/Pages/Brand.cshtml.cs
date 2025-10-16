using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Ui.Class;

namespace Product.Ui.Pages
{
    public class BrandModel : CustomePageModel
    {
        private readonly ApiService _apiService;
        public List<BrandViewModel> brnds { get; set; }
        public BrandModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task OnGet()
        {
            brnds = await  _apiService.GetDataAsync<List<BrandViewModel>>(ProductApiUrl, "api/Brand/GetList");
        }
    }
}
