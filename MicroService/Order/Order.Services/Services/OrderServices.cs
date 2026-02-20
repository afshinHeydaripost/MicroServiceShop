using Helper;
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
    public class OrderServices : IOrderServices
    {
        private readonly MicroServiceShopOrderContext _context;

        public OrderServices(MicroServiceShopOrderContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> CreateOrderForUser(int userId)
        {
            try
            {
                if (await _context.Orders.AnyAsync(x => x.UserId == userId && x.Status == null))
                    return GeneralResponse.Success();
                var obj = new OrderViewModel()
                {
                    Status = OrderStatus.Draft.ToString(),
                    TotalPrice = 0,
                    OrderNo=await  GetMaxOrderNo(),
                    UserId = userId,
                }.ToOrder();
                await _context.AddAsync(obj);
                await _context.SaveChangesAsync();
                return GeneralResponse.Success();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }

        public async Task<GeneralResponse> Delete(Guid id, int userId)
        {
            try
            {
                var obj = await GetById(id, true);
                if (obj == null)
                    return GeneralResponse.NotFound();
                _context.Remove(obj);
                await _context.SaveChangesAsync();
                return GeneralResponse.Success();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }
        private async Task<DataModel.Models.Order> GetById(Guid id, bool isModel)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }
        public async Task<OrderViewModel> GetItem(int id)
        {
            throw new NotImplementedException();
        }
        private async Task<string> GetMaxOrderNo()
        {
            var code = "0000001";
            try
            {
                if (await _context.Orders.AnyAsync())
                {
                    var pCode = await _context.Orders.MaxAsync(x => Convert.ToInt32(x.OrderNo));
                    code = (pCode + 1).ToString().PadLeft(7, '0');
                }
            }
            catch (Exception e)
            {
                code = "0000001";
            }
            return code;
        }
        public async Task<List<OrderViewModel>> GetListForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> Update(OrderViewModel item)
        {
            try
            {
                var obj = await GetById(item.OrderId, true);
                if (obj == null)
                    return GeneralResponse.NotFound();
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
