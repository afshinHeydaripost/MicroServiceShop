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
            item.Code = await GetProductMaxCode();
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
    private async Task<string> GetProductMaxCode()
    {
        var code = "000001";
        try
        {
            if (await _context.Products.AnyAsync())
            {
                var pCode = await _context.Products.MaxAsync(x => int.Parse(x.Code));
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
            _context.Products.Update(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

