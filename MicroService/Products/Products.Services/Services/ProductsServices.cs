using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;

namespace Products.Services;

public class ProductsServices : IProductsServices
{
    private readonly MicroServiceShopContext _context;
    public ProductsServices(MicroServiceShopContext context)
    {
        _context = context;
    }
    public async Task<GeneralResponse> Create(ProductViewModel item)
    {
        try
        {
            item.ProductId = await GetMaxId();
            var obj = item.ToProduct();
            await _context.Products.AddAsync(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    private async Task<int> GetMaxId()
    {
        var id = 0;
        if (await _context.Products.AnyAsync())
        {
            id = await _context.Products.MaxAsync(x => x.ProductId);
        }
        return id + 1;
    }
    private async Task<Product> GetById(int id, bool isModel)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
    }
    public async Task<GeneralResponse> Delete(int userId, int id)
    {
        try
        {
            var item = await GetById(id, true);
            if (item == null)
                return GeneralResponse.NotFound();
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<ProductViewModel> GetItem(int id)
    {
        var item = await _context.Products.Where(x => x.ProductId == id).Select(x => new ProductViewModel()
        {

            IsHidden = x.IsHidden,

            ProductId = x.ProductId,
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
        var query = _context.Products.Select(x => new ProductViewModel()
        {
            IsHidden = x.IsHidden,

            ProductId = x.ProductId,
            BrandId = x.BrandId,
            CategoryId = x.CategoryId,
            Code = x.Code,
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
            var obj = await GetById(item.ProductId, true);
            if (obj == null)
                return GeneralResponse.NotFound();
            obj.IsHidden = item.IsHidden;
            obj.BrandId = item.BrandId;
            obj.CategoryId = item.CategoryId;
            obj.Code = item.Code;
            obj.Description = item.Description;
            obj.Picture = item.Picture;
            obj.Title = item.Title;
            obj.UpdateDate = DateTime.Now;
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

