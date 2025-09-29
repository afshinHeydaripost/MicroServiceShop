﻿using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductStock
{
    public int ProductStockId { get; set; }

    public int ProductModelId { get; set; }

    public int Amount { get; set; }

    public int? InvoiceId { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ProductModel ProductModel { get; set; }
}
