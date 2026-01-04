using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductModel
{
    public int ProductModelId { get; set; }

    public int ProductId { get; set; }

    public int? ColorId { get; set; }

    public int? Price { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ProductColor Color { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual Product Product { get; set; }

    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}
