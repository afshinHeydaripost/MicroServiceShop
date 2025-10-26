using Helper;
using Helper.VieModels;
using Order.DataModel.Context;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services.Services
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly MicroServiceShopOrderContext _context;

        public OrderItemServices(MicroServiceShopOrderContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> Create(OrderItemViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Delete(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItemViewModel> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderItemViewModel>> GetListForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(OrderItemViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
