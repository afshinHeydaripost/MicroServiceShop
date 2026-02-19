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
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string OrderDateFa { get; set; } = null!;

        public decimal TotalPrice { get; set; }
        public string strTotalPrice { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
    }
    public  class OrderItemViewModel
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductTitle { get; set; }

        public string ColorTitle { get; set; }

        public string CategotyTitle { get; set; }

        public string BrandTitle { get; set; }

        public decimal Price { get; set; }
        public string StrPrice => UnitPrice.ToString("N0");
        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public string StrUnitPrice => UnitPrice.ToString("N0");


    }
}
