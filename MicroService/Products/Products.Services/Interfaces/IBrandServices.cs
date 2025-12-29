using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

public interface IBrandServices
{
    Task<GeneralResponse> Create(BrandViewModel item);
    Task<GeneralResponse> Update(BrandViewModel item);
    Task<GeneralResponse> Delete(int id,int userId);
    Task<BrandViewModel> GetItem(int id);
    Task<List<BrandViewModel>> GetList(int userId,bool showAll=true, string text = "");
}

