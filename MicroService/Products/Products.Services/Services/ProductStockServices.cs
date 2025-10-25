using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;

namespace Products.Services;

public class ProductStockServices : IProductStockServices
{
    private readonly MicroServiceShopContext _context;
    public ProductStockServices(MicroServiceShopContext context)
    {
        _context = context;
    }
    public async Task<GeneralResponse> Create(ProductStockViewModel item)
    {
        try
        {
            var obj = item.ToProductStock();
            await _context.ProductStocks.AddAsync(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
    
}

