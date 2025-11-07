using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;

namespace Products.Services;
public class ProductCategoryServices : IProductCategoryServices
{
    private readonly MicroServiceShopContext _context;
    public ProductCategoryServices(MicroServiceShopContext context)
    {
        _context = context;
    }

    
    public async Task<GeneralResponse> Create(ProductCategoryViewModel item)
    {
        try
        {
            var obj = item.ToProductCategory();
            await _context.ProductCategories.AddAsync(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
    private async Task<ProductCategory> GetById(int id, bool isModel)
    {
        return await _context.ProductCategories.FirstOrDefaultAsync(x => x.ProductCategoryId == id);
    }
    public async Task<GeneralResponse> Delete(int id, int userId)
    {
        try
        {
            var item = await GetById(id, true);
            if (item == null)
                return GeneralResponse.NotFound();
            _context.ProductCategories.Remove(item);
            await _context.SaveChangesAsync();
            return GeneralResponse.SuccessDelete();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<ProductCategoryViewModel> GetItem(int id)
    {
        var item = await _context.ProductCategories.Where(x => x.ProductCategoryId == id).Select(x => new ProductCategoryViewModel()
        {
            ProductCategoryId = x.ProductCategoryId,
            IsHidden = x.IsHidden,
            ImageUrl = x.ImageUrl,
            OrderView = x.OrderView,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductCategoryViewModel>> GetList(int userId, string text = "")
    {
        var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            ProductCategoryId = x.ProductCategoryId,
            IsHidden = x.IsHidden,
            ImageUrl = x.ImageUrl,
            OrderView = x.OrderView,
            Title = x.Title,
        }).AsQueryable();
        if (!string.IsNullOrEmpty(text))
        {
            query = query.Where(x => x.Title.Contains(text)).AsQueryable();
        }
        return await query.ToListAsync();
    }

    public async Task<GeneralResponse> Update(ProductCategoryViewModel item)
    {
        try
        {
            var obj = await GetById(item.ProductCategoryId, true);
            if (obj == null)
                return GeneralResponse.NotFound();

            obj.Title = item.Title;
            obj.ImageUrl = item.ImageUrl;
            obj.IsHidden = item.IsHidden;
            obj.OrderView = item.OrderView;
            obj.UpdateDate = DateTime.Now;
            _context.ProductCategories.Update(obj);
            await _context.SaveChangesAsync();

            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

