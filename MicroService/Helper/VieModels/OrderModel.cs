using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class OrderModel
    {
    }
    public  class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderDateFa { get; set; } = null!;

        public decimal TotalPrice { get; set; }
        public string strTotalPrice { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
    }
    public  class OrderItemViewModel
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductModelId { get; set; }

        public string ProductTitle { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal strUnitPrice { get; set; }

    }
}
