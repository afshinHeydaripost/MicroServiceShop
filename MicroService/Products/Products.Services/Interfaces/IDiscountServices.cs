using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

public interface IDiscountServices
{
    Task<GeneralResponse> Create(DiscountViewModel item);
    Task<GeneralResponse> Update(DiscountViewModel item);
    Task<GeneralResponse> Delete(int id,int userId);
    Task<DiscountViewModel> GetItem(int id);
    Task<List<DiscountViewModel>> GetList(int userId,bool showAll=true, string text = "");
    Task<List<DiscountViewModel>> GetActiveList();
}

