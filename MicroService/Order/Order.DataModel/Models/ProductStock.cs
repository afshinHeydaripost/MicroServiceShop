using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class ProductStock
{
    public int ProductStockId { get; set; }

    public int ProductInfoId { get; set; }

    public int Amount { get; set; }

    public Guid? OrderId { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Order Order { get; set; }

    public virtual ProductInfo ProductInfo { get; set; }
}
