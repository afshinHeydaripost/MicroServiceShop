using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Products.DataModel.Context;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using Products.Services.Tools;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Products.Services;
public class DiscountServices : GeneralServices<Discount>, IDiscountServices
{
    private readonly MicroServiceShopContext _context;

    public DiscountServices(MicroServiceShopContext Context) : base(Context)
    {
    }
    public async Task<GeneralResponse> Create(DiscountViewModel item)
    {
        if ((item.DiscountRate == null || item.DiscountRate == 0) && (string.IsNullOrEmpty(item.StrDiscountPrice) || item.StrDiscountPrice == "0"))
        {
            return GeneralResponse.Fail("مبلغ و درصد تخفیف معتبرنیست");
        }
        if (string.IsNullOrEmpty(item.ValidityDate) || string.IsNullOrEmpty(item.ValidityTime))
            return GeneralResponse.Fail("تاریخ و زمان اعتبار را وارد کنید");
        try
        {
            if (!string.IsNullOrEmpty(item.StrDiscountPrice))
                item.DiscountPrice = int.Parse(item.StrDiscountPrice.Replace(",", ""));
            var obj = item.ToDiscount();
            obj.ValidityDate = DateTools.ToDateTime(item.ValidityDate + "-" + item.ValidityTime);
            return await  Add(obj);
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

    public async Task<DiscountViewModel> GetItem(int id)
    {
        var item = await _context.Discounts.Where(x => x.Id == id).Select(x => new DiscountViewModel()
        {
            BrandId = x.BrandId,

            Active = x.Active,
            DiscountId = x.Id,
            ProductCategoryId = x.ProductCategoryId,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            BrandTitle = x.Brand.Title,
            ProductCategoryTitle = x.ProductCategory.Title,
            ProductModelTitle = x.ProductModel.Color.Title,
            ValidityTime = x.ValidityDate.ToTimeFa(),
            ValidityDate = x.ValidityDate.ToDateFa(),
            ValidityDateTime = x.ValidityDate.ToDateTimeFa(),
            ProductTitle = x.Product.Title,
            DiscountPrice = x.DiscountPrice,
            DiscountRate = x.DiscountRate,
            Title = x.Title,
        }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<List<DiscountViewModel>> GetList(int userId, bool showAll = true, string text = "")
    {
        var query = _context.Discounts.Select(x => new DiscountViewModel()
        {
            Title = x.Title,
            BrandId = x.BrandId,
            Active = x.Active,
            DiscountId = x.Id,
            ProductCategoryId = x.ProductCategoryId,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            BrandTitle = x.Brand.Title,
            ValidityTime = x.ValidityDate.ToTimeFa(),
            ValidityDate = x.ValidityDate.ToDateFa(),
            ValidityDateTime = x.ValidityDate.ToDateTimeFa(),
            ProductCategoryTitle = x.ProductCategory.Title,
            ProductModelTitle = x.ProductModel.Color.Title,
            ProductTitle = x.Product.Title,
            DiscountPrice = x.DiscountPrice,
            DiscountRate = x.DiscountRate,
        }).AsQueryable();
        if (!string.IsNullOrEmpty(text))
        {
            query = query.Where(x => x.Title.Contains(text)).AsQueryable();
        }
        return await query.ToListAsync();
    }

    public async Task<GeneralResponse> Update(DiscountViewModel item)
    {
        if ((item.DiscountRate == null || item.DiscountRate == 0) && (string.IsNullOrEmpty(item.StrDiscountPrice) || item.StrDiscountPrice == "0"))
        {
            return GeneralResponse.Fail("مبلغ و درصد تخفیف معتبرنیست");
        }
        if (string.IsNullOrEmpty(item.ValidityDate) || string.IsNullOrEmpty(item.ValidityTime))
            return GeneralResponse.Fail("تاریخ و زمان اعتبار را وارد کنید");
        try
        {
            if (!string.IsNullOrEmpty(item.StrDiscountPrice))
                item.DiscountPrice = int.Parse(item.StrDiscountPrice.Replace(",", ""));
            var obj = await GetById(item.DiscountId);
            if (obj == null)
                return GeneralResponse.NotFound();
            obj.Title = item.Title;
            obj.ValidityDate = DateTools.ToDateTime(item.ValidityDate + "-" + item.ValidityTime);
            obj.BrandId = item.BrandId;
            obj.DiscountRate = item.DiscountRate;
            obj.DiscountPrice = item.DiscountPrice;
            obj.Active = item.Active;
            obj.ProductCategoryId = item.ProductCategoryId;
            obj.ProductId = item.ProductId;
            obj.ProductModelId = item.ProductModelId;
            obj.Title = item.Title;
            obj.UpdateDate = DateTime.Now;
            return await Edit(obj);
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async  Task<List<DiscountViewModel>> GetActiveList()
    {
        var query = _context.Discounts.Where(x=>x.Active && x.ValidityDate>DateTime.Now).Select(x => new DiscountViewModel()
        {
            Title = x.Title,
            BrandId = x.BrandId,
            Active = x.Active,
            ProductCategoryId = x.ProductCategoryId,
            ProductId = x.ProductId,
            ProductModelId = x.ProductModelId,
            BrandTitle = x.Brand.Title,
            ValidityTime = x.ValidityDate.ToTimeFa(),
            ValidityDate = x.ValidityDate.ToDateFa(),
            ValidityDateTime = x.ValidityDate.ToDateTimeFa(),
            ProductCategoryTitle = x.ProductCategory.Title,
            ProductModelTitle = x.ProductModel.Color.Title,
            ProductTitle = x.Product.Title,
            DiscountPrice = x.DiscountPrice,
            DiscountRate = x.DiscountRate,
        }).AsQueryable();
        return await query.ToListAsync();
    }
}

