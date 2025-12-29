using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

public interface IProductCategoryServices
    {
        Task<GeneralResponse> Create(ProductCategoryViewModel item);
        Task<GeneralResponse> Update(ProductCategoryViewModel item);
        Task<GeneralResponse> Delete(int id ,int userId);
        Task<ProductCategoryViewModel> GetItem(int id);
        Task<List<ProductCategoryViewModel>> GetList(int userId, bool showAll = true, string text = "");
    }

