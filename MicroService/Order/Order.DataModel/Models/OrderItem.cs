using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class OrderItem
{
    public OrderItem()
    {
        OrderItemId = Guid.NewGuid();
    }
    public Guid OrderItemId { get; set; }

    public Guid OrderId { get; set; }

    public int ProductId { get; set; }

    public string ProductCode { get; set; }

    public string ProductTitle { get; set; }

    public string ColorTitle { get; set; }

    public string CategotyTitle { get; set; }

    public string BrandTitle { get; set; }

    public decimal Price { get; set; }

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Order Order { get; set; }

    public virtual ProductInfo Product { get; set; }
}
