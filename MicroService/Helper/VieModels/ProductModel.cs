using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    public class ProductModel
    {
        public List<ProductViewModel> ProductsList { get; set; }
        public List<ProductCategoryViewModel> ProductCategoryList { get; set; }
        public List<ProductColorViewModel> ProductColors { get; set; }
        public List<BrandViewModel> BrandsList { get; set; }
        public ProductViewModel Product { get; set; }
        public ProductModelViewMode ProductModels { get; set; }
    }
    public partial class ProductViewModel : GeneralProp
    {
        public int? ProductId { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "گروه کالا")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int? CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        [Display(Name = "برند")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int? BrandId { get; set; }
        public string BrandTitle { get; set; }

        public string Code { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]
        public string Title { get; set; }

        public string Picture { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]

        public string Description { get; set; }

        public bool IsHidden { get; set; }
        public decimal? Rate { get; set; }

        public resBestPrice Price { get; set; }
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
        public string strPrice => (Price != null) ? Price.Value.ToString("N0") : "";


        public int? BrandId { get; set; }
    }
    public class ProductStockViewModel
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
    public class resBestPrice
    {
        public int Price { get; set; }
        public string strPrice => Price.ToString("N0");
        public int BasePrice { get; set; }
        public string strBasePrice => BasePrice.ToString("N0");
        public int DiscountID { get; set; }
        public float DisPercent { get; set; }
        public bool HasDiscount => (DiscountID!=0 && Price != BasePrice) ? true : false;
    }
}
