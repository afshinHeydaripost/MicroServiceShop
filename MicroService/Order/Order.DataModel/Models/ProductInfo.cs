using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class ProductInfo
{
    public int ProductInfoId { get; set; }

    public int ProductId { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public string Picture { get; set; }

    public bool ProductIsHidden { get; set; }

    public int ProductModelId { get; set; }

    public decimal Price { get; set; }

    public int ColorId { get; set; }

    public string ColorTitle { get; set; }

    public int CategoryId { get; set; }

    public string CategotyTitle { get; set; }

    public bool CategotyIsHidden { get; set; }

    public string CategotyImageUrl { get; set; }

    public int BrandId { get; set; }

    public string BrandTitle { get; set; }

    public string BrandLogo { get; set; }

    public bool BrandIsHidden { get; set; }

    public DateTime CreateDateTime { get; set; }

    public DateTime? LastUpdateDateTime { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}
