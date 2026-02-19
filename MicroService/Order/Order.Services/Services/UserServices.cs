using Helper;
using Helper.VieModels;
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
    public class UserServices : IUserServices
    {
        private readonly MicroServiceShopOrderContext _context;

        public UserServices(MicroServiceShopOrderContext context)
        {
            _context = context;
        }
        public async  Task<GeneralResponse> Create(UserViewModel item)
        {
            try
            {
                var obj = item.ToUser();
                await _context.Users.AddAsync(obj);
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
