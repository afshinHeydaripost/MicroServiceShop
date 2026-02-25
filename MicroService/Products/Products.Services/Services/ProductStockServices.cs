using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;

namespace Products.Services;

public class ProductStockServices : GeneralServices<ProductStock>, IProductStockServices
{
    public ProductStockServices(MicroServiceShopContext Context) : base(Context)
    {
    }

    public async Task<GeneralResponse> Create(ProductStockViewModel item)
    {
        try
        {
            var obj = item.ToProductStock();
            return await Add(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

}

