using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public byte[] OrderDateFa { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
