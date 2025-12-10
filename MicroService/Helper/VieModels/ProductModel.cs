using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    public class ProductModel
    {
        public List<ProductViewModel> ProductsList { get; set; }
        public List<ProductCategoryViewModel> ProductCategoryList { get; set; }
        public List<BrandViewModel> BrandsList { get; set; }
        public ProductViewModel Product { get; set; }
    }
    public partial class ProductViewModel
    {
        public int ProductId { get; set; }

        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int? BrandId { get; set; }
        public string BrandTitle { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public bool? IsHidden { get; set; }

    }
    public partial class ProductModelViewMode
    {
        public int? ProductModelId { get; set; }

        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductCode { get; set; }

        public int? ColorId { get; set; }
        public string ColorTitle { get; set; }

        public int? Amount { get; set; }

        public int? Price { get; set; }
    }
    public  class ProductStockViewModel
    {
        public int ProductModelId { get; set; }

        public int? Amount { get; set; }

        public int? OrderId { get; set; }

        public DateTime UpdateDate { get; set; }
    }
    public partial class ProductInfoViewModel
    {
        public int ProductInfoId { get; set; }
        public int ProductID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public bool ProductIsHidden { get; set; }
        public int ProductModelID { get; set; }
        public decimal Price { get; set; }
        public int ColorID { get; set; }
        public string ColorTitle { get; set; }
        public int CategoryID { get; set; }
        public string CategotyTitle { get; set; }
        public bool CategotyIsHidden { get; set; }
        public string CategotyImageUrl { get; set; }
        public int BrandID { get; set; }
        public string BrandTitle { get; set; }
        public string BrandLogo { get; set; }
        public bool BrandIsHidden { get; set; }
    }

}
