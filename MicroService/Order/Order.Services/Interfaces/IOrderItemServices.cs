using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services.Interfaces;

public interface IOrderItemServices
{
    Task<GeneralResponse> Create(OrderItemViewModel item);
    Task<GeneralResponse> Update(OrderItemViewModel item);
    Task<GeneralResponse> Delete(int id,int userId);
    Task<OrderItemViewModel> GetItem(int id);
    Task<List<OrderItemViewModel>> GetListForUser(int userId);
}

