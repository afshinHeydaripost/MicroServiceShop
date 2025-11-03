using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderDateFa { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }
    public string Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}
