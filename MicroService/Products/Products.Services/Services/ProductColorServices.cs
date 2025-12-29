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
public class ProductColorServices : IProductColorServices
{
    private readonly MicroServiceShopContext _context;

    public ProductColorServices(MicroServiceShopContext context)
    {
        _context = context;
    }
    public async Task<GeneralResponse> Create(ProductColorViewModel item)
    {
        try
        {
            var obj = item.ToProductColor();
            await _context.ProductColors.AddAsync(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
    private async Task<ProductColor> GetById(int id, bool isModel)
    {
        return await _context.ProductColors.FirstOrDefaultAsync(x => x.ProductColorId == id);
    }
    public async Task<GeneralResponse> Delete(int id, int userId)
    {
        try
        {
            var item = await GetById(id, true);
            if (item == null)
                return GeneralResponse.NotFound();
            _context.ProductColors.Remove(item);
            await _context.SaveChangesAsync();
            return GeneralResponse.SuccessDelete();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<ProductColorViewModel> GetItem(int id)
    {
        var item = await _context.ProductColors.Where(x => x.ProductColorId == id).Select(x => new ProductColorViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductColorId = x.ProductColorId,
            Rgb = x.Rgb,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<ProductColorViewModel>> GetList(int userId, bool showAll = true, string text = "")
    {

        var query = _context.ProductColors.Select(x => new ProductColorViewModel()
        {
            IsHidden = x.IsHidden ?? false,
            ProductColorId = x.ProductColorId,
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

            var obj = await GetById(item.ProductColorId ?? 0, true);
            if (obj == null)
                return GeneralResponse.NotFound();

            obj.Title = item.Title;
            obj.IsHidden = item.IsHidden;
            obj.Rgb = item.Rgb;
            obj.UpdateDate = DateTime.Now;
            _context.ProductColors.Update(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

