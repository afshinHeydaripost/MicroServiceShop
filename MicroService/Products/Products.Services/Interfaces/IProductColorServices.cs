using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

public interface IProductColorServices
{
    Task<GeneralResponse> Create(ProductColorViewModel item);
    Task<GeneralResponse> Update(ProductColorViewModel item);
    Task<GeneralResponse> Delete(int id,int userId);
    Task<ProductColorViewModel> GetItem(int id);
    Task<List<ProductColorViewModel>> GetList(int userId, bool showAll = true, string text = "");
}

