using Helper.Base;
using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductColor : BaseEntity
{

    public string Title { get; set; }

    public string Rgb { get; set; }

    public bool? IsHidden { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<ProductModel> ProductModels { get; set; } = new List<ProductModel>();
}
