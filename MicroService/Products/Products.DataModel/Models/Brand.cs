using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Title { get; set; }

    public string Logo { get; set; }

    public bool? IsHidden { get; set; }

    public int? OrderView { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
