using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public string Picture { get; set; }

    public DateTime UpdateDate { get; set; }

    public string Description { get; set; }

    public bool? IsHidden { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ProductCategory Category { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<ProductModel> ProductModels { get; set; } = new List<ProductModel>();
}
