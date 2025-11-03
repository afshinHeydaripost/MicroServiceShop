using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class ProductStock
{
    public int ProductStockId { get; set; }

    public int ProductModelId { get; set; }

    public int Amount { get; set; }

    public int? OrderId { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Order Order { get; set; }
}
