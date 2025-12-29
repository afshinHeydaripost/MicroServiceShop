using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class ProductColorModel
    {
    }

    public partial class ProductColorViewModel
    {
        public int? ProductColorId { get; set; }

        public string Title { get; set; }

        public string Rgb { get; set; }

        public bool IsHidden { get; set; }
    }
}
