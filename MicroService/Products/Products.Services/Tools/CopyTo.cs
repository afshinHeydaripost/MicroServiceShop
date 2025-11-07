using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Helper.VieModels;
using Products.DataModel.Models;
namespace Products.Services.Tools;

internal static class CopyTo
{
    internal static Brand ToBrand(this BrandViewModel x)
    {
        return new Brand()
        {
            BrandId = x.BrandId,
            IsHidden = x.IsHidden,
            Logo = x.Logo,
            OrderView = x.OrderView,
            Title = x.Title,
            UpdateDate = DateTime.Now
        };
    }
    internal static ProductColor ToProductColor(this ProductColorViewModel x)
    {
        return new ProductColor()
        {

            IsHidden = x.IsHidden,
            Rgb = x.Rgb,
            ProductColorId = x.ProductColorId ?? 0,
            Title = x.Title,
            UpdateDate = DateTime.Now
        };
    }
    internal static ProductCategory ToProductCategory(this ProductCategoryViewModel x)
    {
        return new ProductCategory()
        {
            ProductCategoryId = x.ProductCategoryId,
            ImageUrl = x.ImageUrl,
            IsHidden = x.IsHidden,
            OrderView = x.OrderView,
            Title = x.Title,
            UpdateDate = DateTime.Now
        };
    }
    internal static Product ToProduct(this ProductViewModel x)
    {
        return new Product()
        {
            BrandId = x.BrandId,
            CategoryId = x.CategoryId,
            Code = x.Code,
            Description = x.Description,
            ProductId = x.ProductId,
            IsHidden = x.IsHidden,
            Title = x.Title,
            UpdateDate = DateTime.Now
        };
    }
    internal static ProductModel ToProductModel(this ProductModelViewMode x)
    {
        return new ProductModel()
        {
            ColorId = x.ColorId,
            ProductId = x.ProductId,
            Price = x.Price,
            UpdateDate = DateTime.Now
        };
    }
    internal static ProductStock ToProductStock(this ProductStockViewModel x)
    {
        return new ProductStock()
        {
            Amount = x.Amount ?? 0,
            ProductModelId = x.ProductModelId,
            OrderId = x.OrderId,
            UpdateDate = DateTime.Now
        };
    }
}
