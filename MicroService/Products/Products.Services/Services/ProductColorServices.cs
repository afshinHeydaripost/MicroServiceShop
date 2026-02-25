using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;
using System;

namespace Products.Services;
public class ProductColorServices : GeneralServices<ProductColor>, IProductColorServices
{
    public ProductColorServices(MicroServiceShopContext Context) : base(Context)
    {
    }
    public async Task<GeneralResponse> Create(ProductColorViewModel item)
    {
        try
        {
            var obj = item.ToProductColor();
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

    public async Task<ProductColorViewModel> GetItem(int id)
    {
        var item = await _Context.ProductColors.Where(x => x.Id == id).Select(x => new ProductColorViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductColorId = x.Id,
            Rgb = x.Rgb,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductColorViewModel>> GetList(int userId, bool showAll = true, string text = "")
    {

        var query = _Context.ProductColors.Select(x => new ProductColorViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductColorId = x.Id,
            Rgb = x.Rgb,
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

    public async Task<GeneralResponse> Update(ProductColorViewModel item)
    {
        try
        {

            var obj = await GetById(item.ProductColorId ?? 0);
            if (obj == null)
                return GeneralResponse.NotFound();

            obj.Title = item.Title;
            obj.IsHidden = item.IsHidden;
            obj.Rgb = item.Rgb;
            obj.UpdateDate = DateTime.Now;
            _Context.ProductColors.Update(obj);
            await _Context.SaveChangesAsync();
            return await Edit(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

