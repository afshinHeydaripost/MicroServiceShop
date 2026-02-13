using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Order.DataModel.Context;
using Order.DataModel.Models;
using Order.Services.Interfaces;
using Order.Services.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services.Services
{
    public class ProductInfoServices : IProductInfoServices
    {
        private readonly MicroServiceShopOrderContext _context;

        public ProductInfoServices(MicroServiceShopOrderContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> Create(ProductInfoViewModel item)
        {
            try
            {
                var obj = item.ToProductInfo();
                await _context.ProductInfo.AddAsync(obj);
                await _context.SaveChangesAsync();
                return GeneralResponse.Success();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }
        private async Task<ProductInfo> GetByProductModelID(int id, bool isModel)
        {
            return await _context.ProductInfo.FirstOrDefaultAsync(x => x.ProductModelID == id);
        }
        public async Task<GeneralResponse> Update(ProductInfoViewModel item)
        {
            try
            {
                var obj = await GetByProductModelID(item.ProductModelID, true);
                if (obj == null)
                    return await Create(item);
                obj.ColorTitle = item.ColorTitle;
                obj.Price = item.Price;
                obj.Title = item.Title;
                obj.BrandID = item.BrandID;
                obj.BrandIsHidden = item.BrandIsHidden;
                obj.BrandLogo = item.BrandLogo;
                obj.BrandTitle = item.BrandTitle;
                obj.CategoryID = item.CategoryID;
                obj.CategotyImageUrl = item.CategotyImageUrl;
                obj.CategotyIsHidden = item.CategotyIsHidden;
                obj.CategotyTitle = item.CategotyTitle;
                obj.Code = item.Code;
                obj.ColorID = item.ColorID;
                obj.Picture = item.Picture;
                obj.ProductID = item.ProductID;
                obj.ProductIsHidden = item.ProductIsHidden;
                obj.ProductModelID = item.ProductModelID;
                obj.LastUpdateDateTime = DateTime.Now;
                _context.ProductInfo.Update(obj);
                await _context.SaveChangesAsync();
                return GeneralResponse.Success();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }

        public async  Task<GeneralResponse> Delete(int id)
        {
            try
            {
                var obj = await GetByProductModelID(id, true);
                if (obj == null)
                    return GeneralResponse.NotFound();
                _context.ProductInfo.Remove(obj);
                await _context.SaveChangesAsync();
                return GeneralResponse.SuccessDelete();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }

        public async  Task<GeneralResponse> UpdateProductStock(ProductModelViewMode item)
        {
            try
            {
                var obj = item.ToProductStock();
                await _context.ProductStocks.AddAsync(obj);
                await _context.SaveChangesAsync();
                return GeneralResponse.Success();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }
    }
}
