using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class ProductCategoryModel
    {
    }
    public partial class ProductCategoryViewModel
    {
        public int ProductCategoryId { get; set; }

        public string Title { get; set; }

        public bool? IsHidden { get; set; }

        public int? OrderView { get; set; }

        public string ImageUrl { get; set; }
    }

}
