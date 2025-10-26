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
    public class OrderServices : IOrderServices
    {
        private readonly MicroServiceShopOrderContext _context;

        public OrderServices(MicroServiceShopOrderContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> Create(OrderViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Delete(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetListForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(OrderViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
