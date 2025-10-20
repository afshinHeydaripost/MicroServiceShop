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
public class BrandServices : IBrandServices
{
    private readonly MicroServiceShopContext _context;

    public BrandServices(MicroServiceShopContext context)
    {
        _context = context;
    }
    public async Task<GeneralResponse> Create(BrandViewModel item)
    {
        try
        {
            var obj = item.ToBrand();
            await _context.Brands.AddAsync(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
    private async Task<Brand> GetById(int id, bool isModel)
    {
        return await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == id);
    }
    public async Task<GeneralResponse> Delete(int id, int userId)
    {
        try
        {
            var item = await GetById(id, true);
            if (item == null)
                return GeneralResponse.NotFound();
            _context.Brands.Remove(item);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<BrandViewModel> GetItem(int id)
    {
        var item = await _context.Brands.Where(x => x.BrandId == id).Select(x => new BrandViewModel()
        {
            BrandId = x.BrandId,
            IsHidden = x.IsHidden,
            Logo = x.Logo,
            OrderView = x.OrderView,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<BrandViewModel>> GetList(int userId, string text = "")
    {

        var query = _context.Brands.Select(x => new BrandViewModel()
        {
            BrandId = x.BrandId,
            IsHidden = x.IsHidden,
            Logo = x.Logo,
            OrderView = x.OrderView,
            Title = x.Title,
        }).AsQueryable();
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

            var obj = await GetById(item.BrandId, true);
            if (obj == null)
                return GeneralResponse.NotFound();

            obj.Title = item.Title;
            obj.Logo = item.Logo;
            obj.IsHidden = item.IsHidden;
            obj.OrderView = item.OrderView;
            obj.UpdateDate = DateTime.Now;
            _context.Brands.Update(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

