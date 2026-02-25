using Helper;
using Helper.Base;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;
using System.Collections.Generic;

namespace Products.Services;

public class ProductsServices : GeneralServices<Product>, IProductsServices
{
    public ProductsServices(MicroServiceShopContext Context) : base(Context)
    {
    }
    public async Task<GeneralResponse> Create(ProductViewModel item)
    {
        try
        {
            item.Code = await GetProductMaxCode();
            var obj = item.ToProduct();
            return await  Add(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    private async Task<List<int>> GetValidProductIds()
    {
        try
        {
            var products = await _Context.ProductsList.FromSqlRaw("exec dbo.GetValidProductIds").ToListAsync();
            return products.Select(x => x.Id).ToList();
        }
        catch (Exception e)
        {
            return new List<int>();
        }
    }
    private async Task<List<int>> GetValidProductModelIds(int productId, int? userId = null)
    {
        try
        {
            var products = await _Context.ProductsList.FromSqlRaw("exec dbo.GetValidProductModelIds @ProductId=@P0,@UserId=@P1", productId, userId).ToListAsync();
            return products.Select(x => x.Id).ToList();
        }
        catch (Exception e)
        {
            return new List<int>();
        }
    }
    public async Task<GeneralResponse<ProductViewModel>> Delete(int userId, int id)
    {
        try
        {
            var item = await GetById(id);
            if (item == null)
                return GeneralResponse<ProductViewModel>.NotFound(new ProductViewModel());
            var obj = await GetItem(id);
            _Context.Products.Remove(item);
            await _Context.SaveChangesAsync();
            return GeneralResponse<ProductViewModel>.Success(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse<ProductViewModel>.Fail(e);
        }
    }
    private async Task<string> GetProductMaxCode()
    {
        var code = "000001";
        try
        {
            if (await _Context.Products.AnyAsync())
            {
                var pCode = await _Context.Products.MaxAsync(x => Convert.ToInt32(x.Code));
                code = (pCode + 1).ToString().PadLeft(6, '0');
            }
        }
        catch (Exception e)
        {
            code = "000001";
        }
        return code;
    }
    public async Task<ProductViewModel> GetItem(int id)
    {
        var item = await _Context.Products.Where(x => x.Id == id).Select(x => new ProductViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductId = x.Id,
            BrandId = x.BrandId,
            CategoryId = x.CategoryId,
            Code = x.Code,
            Description = x.Description,
            Picture = x.Picture,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductViewModel>> GetList(int userId, string text = "")
    {
        var query = _Context.Products.Select(x => new ProductViewModel()
        {
            IsHidden = x.IsHidden ?? false,

            ProductId = x.Id,
            BrandId = x.BrandId,
            CategoryId = x.CategoryId,
            Code = x.Code,
            BrandTitle = x.Brand.Title,
            CategoryTitle = x.Category.Title,
            Description = x.Description,
            Picture = x.Picture,
            Title = x.Title,
        }).AsQueryable();
        if (!string.IsNullOrEmpty(text))
        {
            query = query.Where(x => x.Title.Contains(text) || x.Code.Contains(text)).AsQueryable();
        }
        return await query.ToListAsync();
    }

    public async Task<GeneralResponse> Update(ProductViewModel item)
    {
        try
        {
            var obj = await GetById(item.ProductId ?? 0);
            if (obj == null)
                return GeneralResponse.NotFound();
            obj.IsHidden = item.IsHidden;
            obj.BrandId = item.BrandId;
            obj.CategoryId = item.CategoryId;
            obj.Code = item.Code;
            obj.Description = item.Description;
            if (!string.IsNullOrEmpty(item.Picture))
                obj.Picture = item.Picture;
            obj.Title = item.Title;
            obj.UpdateDate = DateTime.Now;
            return  await Edit(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<string> GetCode(int userId)
    {
        return await GetProductMaxCode();
    }

    public async Task<List<ProductViewModel>> GetValidProductList(string text = "")
    {
        var validIds = await GetValidProductIds();
        var query = _Context.Products.Where(x => validIds.Contains(x.Id)).Select(x => new ProductViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductId = x.Id,
            BrandId = x.BrandId,
            CategoryId = x.CategoryId,
            Code = x.Code,
            BrandTitle = x.Brand.Title,
            CategoryTitle = x.Category.Title,
            Picture = x.Picture,
            Title = x.Title,
        }).AsQueryable();
        if (!string.IsNullOrEmpty(text))
        {
            query = query.Where(x => x.Title.Contains(text) || x.Code.Contains(text)).AsQueryable();
        }
        return CalculatePriceProducts(await  query.ToListAsync(), validIds);
    }
    public async Task<List<ProductViewModel>> GetNewestProductList(int rowInPage = 10, int? userId = null, string text = "")
    {
        var validIds = await GetValidProductIds();
        var query = _Context.Products.Where(x => validIds.Contains(x.Id)).OrderByDescending(x => x.Id).Select(x => new ProductViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductId = x.Id,
            BrandId = x.BrandId,
            CategoryId = x.CategoryId,
            Code = x.Code,
            BrandTitle = x.Brand.Title,
            CategoryTitle = x.Category.Title,
            Picture = x.Picture,
            Title = x.Title,
        }).Take(rowInPage).AsQueryable();
        if (!string.IsNullOrEmpty(text))
        {
            query = query.Where(x => x.Title.Contains(text) || x.Code.Contains(text)).AsQueryable();
        }
        return CalculatePriceProducts(await query.ToListAsync(), validIds);
    }
    private List<ProductViewModel> CalculatePriceProducts(
        List<ProductViewModel> products,
        List<int> validProductIds)
    {
        if (products == null || products.Count == 0)
            return products ?? new List<ProductViewModel>();

        var priceService = new BestPrice(_Context, validProductIds);

        foreach (var product in products.Where(p => p.ProductId.HasValue))
        {
            product.Price = priceService.ByProductID(product.ProductId.Value);
        }

        return products;
    }


}

