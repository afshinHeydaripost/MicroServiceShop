using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;
using System.Threading;

namespace Products.Services;
public class ProductModelServices : GeneralServices<Products.DataModel.Models.ProductModel>, IProductModelServices
{
    public ProductModelServices(MicroServiceShopContext Context) : base(Context)
    {
    }
    public async Task<GeneralResponse<ProductModelViewMode>> Create(ProductModelViewMode item)
    {

        try
        {
            if (await _Context.ProductModels.AnyAsync(x => x.ProductId == item.ProductId && x.ColorId == item.ColorId))
            {
                return GeneralResponse<ProductModelViewMode>.Fail(item, "این رنگ قبلا ثبت شده است");
            }
            var obj = item.ToProductModel();
            await Add(obj);
            if (item.Amount is not null && item.Amount != 0)
            {
                var productStockServices = new ProductStockServices(_Context);
                ProductStockViewModel objProductStock = new ProductStockViewModel()
                {
                    Amount = item.Amount,
                    ProductModelId = obj.Id,
                    UpdateDate = DateTime.Now
                };
                await productStockServices.Create(objProductStock);
            }
            item.ProductModelId = obj.Id;
            return GeneralResponse<ProductModelViewMode>.Success(item);
        }
        catch (Exception e)
        {
            return GeneralResponse<ProductModelViewMode>.Fail(item, e);
        }
    }

    public async Task<GeneralResponse> Delete(int userId, int id)
    {
        return await Delete(id);
    }

    public async Task<ProductModelViewMode> GetItem(int id)
    {
        var item = await _Context.ProductModels.Where(x => x.Id == id).Select(x => new ProductModelViewMode()
        {
            ColorId = x.ColorId,
            ColorTitle = x.Color.Title,
            Price = x.Price ?? 0,
            ProductCode = x.Product.Code,
            ProductId = x.ProductId,
            ProductModelId = x.Id,
            ProductTitle = x.Product.Title
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductModelViewMode>> GetList(int productId, string text = "")
    {
        var query = _Context.ProductModels.Where(x => x.ProductId == productId).Select(x => new ProductModelViewMode()
        {
            ColorId = x.ColorId,
            ColorTitle = x.Color.Title,
            Price = x.Price ?? 0,
            Amount = x.ProductStocks.Sum(z => z.Amount),
            ProductCode = x.Product.Code,
            ProductId = x.ProductId,
            ProductModelId = x.Id,
            ProductTitle = x.Product.Title
        }).AsQueryable();
        return await query.ToListAsync();
    }

    public async Task<GeneralResponse> Update(ProductModelViewMode item)
    {
        try
        {
            var obj = await GetById(item.ProductModelId ?? 0);
            if (obj == null)
                return GeneralResponse.NotFound();
            if (await _Context.ProductModels.AnyAsync(x => x.Id != item.ProductModelId && x.ProductId == item.ProductId && x.ColorId == item.ColorId))
            {
                return GeneralResponse.Fail("این رنگ قبلا ثبت شده است");
            }
            obj.ColorId = item.ColorId;
            obj.Price = item.Price;
            obj.UpdateDate = DateTime.Now;

            var res = await Edit(obj);
            if (!res.isSuccess)
            {
                return res;
            }
            if (item.Amount is not null && item.Amount != 0)
            {
                var productStockServices = new ProductStockServices(_Context);
                ProductStockViewModel objProductStock = new ProductStockViewModel()
                {
                    Amount = item.Amount,
                    ProductModelId = obj.Id,
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
        var item = await _Context.ProductModels.Where(x => x.Id == id).Select(x => new ProductInfoViewModel()
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
            ProductModelID = x.Id
        }).FirstOrDefaultAsync();
        return item;
    }
}

