using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

    public interface IProductsServices
    {
        Task<GeneralResponse> Create(ProductViewModel item);
        Task<GeneralResponse> Update(ProductViewModel item);
        Task<GeneralResponse<ProductViewModel>> Delete(int userId,int id);
        Task<ProductViewModel> GetItem(int id);
        Task<List<ProductViewModel>> GetList(int userId, string text = "");
        Task<string> GetCode(int userId);
    }
