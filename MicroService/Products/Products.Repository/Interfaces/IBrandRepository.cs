using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Repository.Interfaces
{
    public interface IBrandRepository
    {
        Task<GeneralResponse> Create(BrandViewModel item);
        Task<GeneralResponse> Update(BrandViewModel item);
        Task<GeneralResponse> Delete(BrandViewModel item);
        Task<BrandViewModel> GetItem(int id);
        Task<List<BrandViewModel>> GetList(int userId, string text = "");
    }
}
