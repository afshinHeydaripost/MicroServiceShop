using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string Title { get; set; }

    public DateTime ValidityDate { get; set; }

    public int? ProductId { get; set; }

    public int? ProductModelId { get; set; }

    public int? ProductCategoryId { get; set; }

    public int? BrandId { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool Active { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual Product Product { get; set; }

    public virtual ProductCategory ProductCategory { get; set; }

    public virtual ProductModel ProductModel { get; set; }
}
