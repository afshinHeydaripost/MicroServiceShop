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
public class BrandServices : GeneralServices<Brand>, IBrandServices
{
    public BrandServices(MicroServiceShopContext Context) : base(Context)
    {
    }
    public async Task<GeneralResponse> Create(BrandViewModel item)
    {
        try
        {
            var obj = item.ToBrand();
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

    public async Task<BrandViewModel> GetItem(int id)
    {
        var item = await _Context.Brands.Where(x => x.Id == id).Select(x => new BrandViewModel()
        {
            BrandId = x.Id,
            IsHidden = x.IsHidden ?? false,
            Logo = x.Logo,
            OrderView = x.OrderView,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<BrandViewModel>> GetList(int userId, bool showAll = true, string text = "")
    {

        var query = _Context.Brands.Select(x => new BrandViewModel()
        {
            BrandId = x.Id,
            IsHidden = x.IsHidden ?? false,
            Logo = x.Logo,
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

    public async Task<GeneralResponse> Update(BrandViewModel item)
    {
        try
        {

            var obj = await GetById(item.BrandId);
            if (obj == null)
                return GeneralResponse.NotFound();

            obj.Title = item.Title;
            if (!string.IsNullOrEmpty(item.Logo))
                obj.Logo = item.Logo;
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

