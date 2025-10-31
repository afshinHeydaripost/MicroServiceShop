using Helper;
using Helper.Tools;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Order.DataModel.Context;
using Order.Services.Interfaces;
using Order.Services.Tools;
using System;
using System.Collections.Generic;
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
    }
}
