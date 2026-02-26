using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    public class DiscountModel
    {
        public List<DiscountViewModel> List { get; set; }
        public List<ProductCategoryViewModel> ProductCategoryList { get; set; }
        public List<BrandViewModel> BrandsList { get; set; }
        public DiscountViewModel Item { get; set; }
    }
    public class DiscountViewModel
    {
        public int DiscountId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]
        public string Title { get; set; }

        [Display(Name = "تاریخ اعتبار")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]
        public string ValidityDate { get; set; }

        [Display(Name = "زمان اعتیار")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]
        public string ValidityTime { get; set; }
        [Display(Name = "درصد تخفبف")]
        public decimal? DiscountRate { get; set; }


        [Display(Name = "تاریخ و زمان اعتبار")]
        public string ValidityDateTime { get; set; }

        [Display(Name = "مبلغ تخفبف")]
        public string StrDiscountPrice { get; set; }
        [Display(Name = "مبلغ تخفبف")]
        public string StrRiallDiscountPrice => (DiscountPrice != null) ? DiscountPrice.Value.ToString("N0") : "";
        public int? DiscountPrice { get; set; }

        public int? ProductId { get; set; }
        public string ProductTitle { get; set; }

        public int? ProductModelId { get; set; }
        public string ProductModelTitle { get; set; }

        public int? ProductCategoryId { get; set; }
        public string ProductCategoryTitle { get; set; }

        public int? BrandId { get; set; }
        public string BrandTitle { get; set; }

        public string UpdateDate { get; set; }

        public bool Active { get; set; }
    }
}
