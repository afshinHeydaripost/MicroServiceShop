using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class ProductModel
    {
    }
    public partial class ProductViewModel
    {
        public int ProductId { get; set; }

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
        public int ProductModelId { get; set; }

        public int ProductId { get; set; }

        public int? ColorId { get; set; }

        public int? Price { get; set; }
    }

}
