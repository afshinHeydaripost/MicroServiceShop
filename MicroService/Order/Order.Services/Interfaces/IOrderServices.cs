using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services.Interfaces;

public interface IOrderServices
{
    Task<GeneralResponse> Create(OrderViewModel item);
    Task<GeneralResponse> Update(OrderViewModel item);
    Task<GeneralResponse> Delete(int id,int userId);
    Task<OrderViewModel> GetItem(int id);
    Task<List<OrderViewModel>> GetListForUser(int userId);
}

