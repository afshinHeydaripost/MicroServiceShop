using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string Title { get; set; }

    public bool? IsHidden { get; set; }

    public int? OrderView { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
