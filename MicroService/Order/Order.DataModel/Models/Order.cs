using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class Order
{
    public Order()
    {
        OrderId = Guid.NewGuid();
    }
    public Guid OrderId { get; set; }

    public string OrderNo { get; set; }

    public DateTime? OrderDate { get; set; }

    public string OrderDateFa { get; set; }

    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// null=&gt;curent 
    /// </summary>
    public string Status { get; set; }

    public bool Finalized { get; set; }

    public int? FinalizedUserId { get; set; }

    public DateTime? FinalizedDateTime { get; set; }

    public bool Revoked { get; set; }

    public string RevokedUserId { get; set; }

    public DateTime? RevokedDateTime { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();

    public virtual User User { get; set; }
}
