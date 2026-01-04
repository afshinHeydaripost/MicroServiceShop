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
public class DiscountServices : IDiscountServices
{
    private readonly MicroServiceShopContext _context;

    public DiscountServices(MicroServiceShopContext context)
    {
        _context = context;
    }
    public async Task<GeneralResponse> Create(DiscountViewModel item)
    {
        try
        {
            var obj = item.ToDiscount();
            await _context.Discounts.AddAsync(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
    private async Task<Discount> GetById(int id, bool isModel)
    {
        return await _context.Discounts.FirstOrDefaultAsync(x => x.DiscountId == id);
    }
    public async Task<GeneralResponse> Delete(int id, int userId)
    {
        try
        {
            var item = await GetById(id, true);
            if (item == null)
                return GeneralResponse.NotFound();
            _context.Discounts.Remove(item);
            await _context.SaveChangesAsync();
            return GeneralResponse.SuccessDelete();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<DiscountViewModel> GetItem(int id)
    {
        var item = await _context.Discounts.Where(x => x.BrandId == id).Select(x => new DiscountViewModel()
        {
            BrandId = x.BrandId,

            Active = x.Active,
            DiscountId = x.DiscountId,
            ProductCategoryId = x.ProductCategoryId,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            BrandTitle = x.Brand.Title,
            ProductCategoryTitle = x.ProductCategory.Title,
            ProductModelTitle = x.ProductModel.Color.Title,
            ProductTitle = x.Product.Title,

            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<DiscountViewModel>> GetList(int userId, bool showAll = true, string text = "")
    {

        var query = _context.Discounts.Select(x => new DiscountViewModel()
        {
            BrandId = x.BrandId,

            Active = x.Active,
            DiscountId = x.DiscountId,
            ProductCategoryId = x.ProductCategoryId,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            BrandTitle = x.Brand.Title,
            ProductCategoryTitle = x.ProductCategory.Title,
            ProductModelTitle = x.ProductModel.Color.Title,
            ProductTitle = x.Product.Title,
        }).AsQueryable();
        if (!string.IsNullOrEmpty(text))
        {
            query = query.Where(x => x.Title.Contains(text)).AsQueryable();
        }
        return await query.ToListAsync();
    }

    public async Task<GeneralResponse> Update(DiscountViewModel item)
    {
        try
        {

            var obj = await GetById(item.DiscountId, true);
            if (obj == null)
                return GeneralResponse.NotFound();
            obj.Title = item.Title;
            obj.BrandId = item.BrandId;
            obj.Active = item.Active;
            obj.ProductCategoryId = item.ProductCategoryId;
            obj.ProductId = item.ProductId;
            obj.ProductModelId = item.ProductModelId;
            obj.Title = item.Title;
            obj.UpdateDate = DateTime.Now;
            _context.Discounts.Update(obj);
            await _context.SaveChangesAsync();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

