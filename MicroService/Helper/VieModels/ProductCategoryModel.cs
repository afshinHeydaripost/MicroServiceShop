using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    public class ProductCategoryModel
    {
        public List<ProductCategoryViewModel> List { get; set; }
        public ProductCategoryViewModel Item { get; set; }
    }
    public  class ProductCategoryViewModel : GeneralProp
    {
        public int? ProductCategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]

        public string Title { get; set; }

        public bool IsHidden { get; set; }

        public int? OrderView { get; set; }

        public string ImageUrl { get; set; }
    }

}
