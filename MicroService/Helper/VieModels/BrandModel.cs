using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Helper.VieModels
{
    public class BrandModel
    {
        public List<BrandViewModel> BrandsList { get; set; }
        public BrandViewModel Brand { get; set; }
    }
    public partial class BrandViewModel : GeneralProp
    {
        public int BrandId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد ")]
        public string Title { get; set; }

        public string Logo { get; set; }

        public bool IsHidden { get; set; }

        public int? OrderView { get; set; }

    }
}
