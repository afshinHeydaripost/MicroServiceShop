
using Helper.VieModels;
using Order.DataModel.Models;

namespace Order.Services.Tools;

internal static class CopyTo
{
    internal static Order.DataModel.Models.Order ToOrder(this OrderViewModel x)
    {
        return new Order.DataModel.Models.Order()
        {
           OrderDate=x.OrderDate,
           TotalPrice=x.TotalPrice,
           UserId=x.UserId,
           OrderDateFa=x.OrderDateFa
        };
    }
    internal static OrderItem ToBrand(this OrderItemViewModel x)
    {
        return new OrderItem()
        {
            OrderId=x.OrderId,
            ProductId=x.ProductId,
            ProductModelId=x.ProductModelId,
            ProductTitle=x.ProductTitle,
            Quantity=x.Quantity,
            UnitPrice=x.UnitPrice,
            
            
        };
    }
}
