using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class BrandModel
    {
    }
    public partial class BrandViewModel
    {
        public int BrandId { get; set; }

        public string Title { get; set; }

        public string Logo { get; set; }

        public bool? IsHidden { get; set; }

        public int? OrderView { get; set; }

    }
}
