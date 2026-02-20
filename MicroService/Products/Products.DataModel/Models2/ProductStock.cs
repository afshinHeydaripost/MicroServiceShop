using Helper.Base;
using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductStock : BaseEntity
{

    public int ProductModelId { get; set; }

    public int Amount { get; set; }

    public int? OrderId { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ProductModel ProductModel { get; set; }
}
