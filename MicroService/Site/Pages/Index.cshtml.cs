using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Site.Pages
{
    public class IndexModel : PageModel
    {

        #region Prop
        private readonly IConfiguration _config;
        private static string _productsServiceUrl;
        public ProductModel Products { get; set; }
        #endregion
        public IndexModel(IConfiguration config)
        {
            _config = config;
            _productsServiceUrl = _config.GetValue<string>("ApiUrl:ProductsService").ToString();
        }


        public async Task OnGet()
        {
            var listProducts = await _productsServiceUrl.GetData<List<ProductViewModel>>($"api/Products/GetNewestProductList?rowInPage={6}");
            Products = new ProductModel()
            {
                ProductsList = new List<ProductViewModel>(),
            };
            if (listProducts is not null)
                Products.ProductsList.AddRange(listProducts);
        }
    }
}
