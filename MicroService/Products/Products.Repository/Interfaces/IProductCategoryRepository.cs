using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Repository.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<GeneralResponse> Create(ProductCategoryViewModel item);
        Task<GeneralResponse> Update(ProductCategoryViewModel item);
        Task<GeneralResponse> Delete(ProductCategoryViewModel item);
        Task<ProductCategoryViewModel> GetItem(int id);
        Task<List<ProductCategoryViewModel>> GetList(int userId, string text = "");
    }
}
