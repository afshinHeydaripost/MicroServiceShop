
using Helper;
using Helper.VieModels;
using Order.DataModel.Models;

namespace Order.Services.Tools;

internal static class CopyTo
{
    internal static Order.DataModel.Models.Order ToOrder(this OrderViewModel x)
    {
        return new Order.DataModel.Models.Order()
        {
            OrderDate = x.OrderDate,
            TotalPrice = x.TotalPrice,
            UserId = x.UserId,
            OrderDateFa = x.OrderDateFa,
            Finalized = false,
            Revoked = false,
            OrderNo = x.OrderNo,
            Status = OrderStatus.Draft.ToString(),
        };
    }
    internal static OrderItem ToBrand(this OrderItemViewModel x)
    {
        return new OrderItem()
        {
            OrderId = x.OrderId,
            ProductTitle = x.ProductTitle,
            Quantity = x.Quantity,
            UnitPrice = x.UnitPrice,
            ColorTitle = x.ColorTitle,
            BrandTitle = x.BrandTitle,
            CategotyTitle = x.CategotyTitle,
            Price = x.Price,
            ProductCode = x.ProductCode,
            ProductId = x.ProductId,
        };
    }

    internal static User ToUser(this UserViewModel x)
    {
        return new User()
        {
            UserCode = x.UserCode,
            CreateDateTime = DateTime.Now,
            Email = x.Email,
            EmailConfirmed = false,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            PhoneNumberConfirmed = false,
            UserName = x.UserName,
            UpdateDateTime = x.UpdateDateTime,
            UserId = x.Id,
        };

    }
    internal static ProductInfo ToProductInfo(this ProductInfoViewModel x)
    {
        return new ProductInfo()
        {
            ColorTitle = x.ColorTitle,
            Price = x.Price,
            Title = x.Title,
            BrandId = x.BrandID,
            BrandIsHidden = x.BrandIsHidden,
            BrandLogo = x.BrandLogo,
            BrandTitle = x.BrandTitle,
            CategoryId = x.CategoryID,
            CategotyImageUrl = x.CategotyImageUrl,
            CategotyIsHidden = x.CategotyIsHidden,
            CategotyTitle = x.CategotyTitle,
            Code = x.Code,
            ColorId = x.ColorID,
            Picture = x.Picture,
            ProductId = x.ProductID,
            ProductIsHidden = x.ProductIsHidden,
            ProductModelId = x.ProductModelID,
            CreateDateTime = DateTime.Now
        };
    }
    internal static ProductStock ToProductStock(this ProductModelViewMode x)
    {
        return new ProductStock()
        {
            Amount = x.Amount ?? 0,
            ProductInfoId = x.ProductModelId ?? 0,
            UpdateDate = DateTime.Now
        };
    }
}
