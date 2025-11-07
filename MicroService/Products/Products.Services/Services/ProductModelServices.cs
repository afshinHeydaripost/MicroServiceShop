using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;
using System.Threading;

namespace Products.Services;
public class ProductModelServices : IProductModelServices
{
    private readonly MicroServiceShopContext _context;

    public ProductModelServices(MicroServiceShopContext context)
    {
        _context = context;
    }
    public async Task<GeneralResponse<ProductModelViewMode>> Create(ProductModelViewMode item)
    {
        try
        {
            var obj = item.ToProductModel();
            await _context.ProductModels.AddAsync(obj);
            await _context.SaveChangesAsync();
            if (item.Amount is not null && item.Amount > 0)
            {
                var productStockServices = new ProductStockServices(_context);
                ProductStockViewModel objProductStock = new ProductStockViewModel()
                {
                    Amount = item.Amount,
                    ProductModelId = item.ProductModelId ?? 0,
                    UpdateDate = DateTime.Now
                };
                await productStockServices.Create(objProductStock);
            }
            item.ProductModelId = obj.ProductModelId;
            return GeneralResponse<ProductModelViewMode>.Success(item);
        }
        catch (Exception e)
        {
            return GeneralResponse<ProductModelViewMode>.Fail(item, e);
        }
    }
    private async Task<ProductModel> GetById(int id, bool isModel)
    {
        return await _context.ProductModels.FirstOrDefaultAsync(x => x.ProductModelId == id);
    }

    public async Task<GeneralResponse> Delete(int userId, int id)
    {
        try
        {
            var item = await GetById(id, true);
            if (item == null)
                return GeneralResponse.NotFound();
            _context.ProductModels.Remove(item);
            await _context.SaveChangesAsync();
            return GeneralResponse.SuccessDelete();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<ProductModelViewMode> GetItem(int id)
    {
        var item = await _context.ProductModels.Where(x => x.ProductModelId == id).Select(x => new ProductModelViewMode()
        {
            ColorId = x.ColorId,
            ColorTitle = x.Color.Title,
            Price = x.Price ?? 0,
            ProductCode = x.Product.Code,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            ProductTitle = x.Product.Title
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductModelViewMode>> GetList(int productId, string text = "")
    {
        var query = _context.ProductModels.Where(x => x.ProductId == productId).Select(x => new ProductModelViewMode()
        {
            ColorId = x.ColorId,
            ColorTitle = x.Color.Title,
            Price = x.Price ?? 0,
            ProductCode = x.Product.Code,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            ProductTitle = x.Product.Title
        }).AsQueryable();
        return await query.ToListAsync();
    }

    public async Task<GeneralResponse> Update(ProductModelViewMode item)
    {
        try
        {
            var obj = await GetById(item.ProductModelId ?? 0, true);
            if (obj == null)
                return GeneralResponse.NotFound();
            obj.ColorId = item.ColorId;
            obj.Price = item.Price;
            obj.UpdateDate = DateTime.Now;
            _context.ProductModels.Update(obj);
            await _context.SaveChangesAsync();
            if (item.Amount is not null && item.Amount > 0)
            {
                var productStockServices = new ProductStockServices(_context);
                ProductStockViewModel objProductStock = new ProductStockViewModel()
                {
                    Amount = item.Amount,
                    ProductModelId = item.ProductModelId ?? 0,
                    UpdateDate = DateTime.Now
                };
                await productStockServices.Create(objProductStock);
            }
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<ProductInfoViewModel> GetItemInfo(int id)
    {
        var item = await _context.ProductModels.Where(x => x.ProductModelId == id).Select(x => new ProductInfoViewModel()
        {

            ColorTitle = x.Color.Title,
            Price = x.Price ?? 0,
            Title = x.Product.Title,
            BrandID = x.Product.BrandId ?? 0,
            BrandIsHidden = x.Product.Brand.IsHidden ?? false,
            BrandLogo = x.Product.Brand.Logo,
            BrandTitle = x.Product.Brand.Title,
            CategoryID = x.Product.CategoryId ?? 0,
            CategotyImageUrl = x.Product.Category.ImageUrl,
            CategotyIsHidden = x.Product.Category.IsHidden ?? false,
            CategotyTitle = x.Product.Category.Title,
            Code = x.Product.Code,
            ColorID = x.ColorId ?? 0,
            Picture = x.Product.Picture,
            ProductID = x.ProductId,
            ProductInfoId = 0,
            ProductIsHidden = x.Product.IsHidden ?? false,
            ProductModelID = x.ProductModelId
        }).FirstOrDefaultAsync();
        return item;
    }
}

