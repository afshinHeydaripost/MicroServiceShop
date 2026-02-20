using Helper.Base;
using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductCategory : BaseEntity
{

    public string Title { get; set; }

    public bool? IsHidden { get; set; }

    public int? OrderView { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
