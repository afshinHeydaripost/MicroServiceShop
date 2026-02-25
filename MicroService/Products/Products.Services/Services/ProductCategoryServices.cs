using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;

namespace Products.Services;
public class ProductCategoryServices : GeneralServices<ProductCategory>, IProductCategoryServices
{
    public ProductCategoryServices(MicroServiceShopContext Context) : base(Context)
    {
    }


    public async Task<GeneralResponse> Create(ProductCategoryViewModel item)
    {
        try
        {
            var obj = item.ToProductCategory();
            return await Add(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
    public async Task<GeneralResponse> Delete(int id, int userId)
    {
        return await Delete(id);
    }

    public async Task<ProductCategoryViewModel> GetItem(int id)
    {
        var item = await _Context.ProductCategories.Where(x => x.Id == id).Select(x => new ProductCategoryViewModel()
        {
            ProductCategoryId = x.Id,
            IsHidden = x.IsHidden ?? false,
            ImageUrl = x.ImageUrl,
            OrderView = x.OrderView,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductCategoryViewModel>> GetList(int userId, bool showAll = true, string text = "")
    {
        var query = _Context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            ProductCategoryId = x.Id,
            IsHidden = x.IsHidden ?? false,
            ImageUrl = x.ImageUrl,
            OrderView = x.OrderView,
            Title = x.Title,
        }).AsQueryable();
        if (!showAll)
        {
            query = query.Where(x => !x.IsHidden).AsQueryable();
        }
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
            var obj = await GetById(item.ProductCategoryId ?? 0);
            if (obj == null)
                return GeneralResponse.NotFound();

            obj.Title = item.Title;
            if (!string.IsNullOrEmpty(item.ImageUrl))
                obj.ImageUrl = item.ImageUrl;
            obj.IsHidden = item.IsHidden;
            obj.OrderView = item.OrderView;
            obj.UpdateDate = DateTime.Now;
            return await Edit(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

