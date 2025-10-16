using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Ui.Class;

namespace Product.Ui.Pages
{
    public class IndexModel : CustomePageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task OnGet()
        {

        }
    }
}
