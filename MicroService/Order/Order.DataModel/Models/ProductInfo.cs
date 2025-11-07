using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class ProductInfo
{
    public int ProductInfoId { get; set; }
    public int ProductID { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Picture { get; set; }
    public bool ProductIsHidden { get; set; }
    public int ProductModelID { get; set; }
    public decimal Price { get; set; }
    public int ColorID { get; set; }
    public string ColorTitle { get; set; }
    public int CategoryID { get; set; }
    public string CategotyTitle { get; set; }
    public bool CategotyIsHidden { get; set; }
    public string CategotyImageUrl { get; set; }
    public int BrandID { get; set; }
    public string BrandTitle { get; set; }
    public string BrandLogo { get; set; }
    public bool BrandIsHidden { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? LastUpdateDateTime { get; set; }


}
