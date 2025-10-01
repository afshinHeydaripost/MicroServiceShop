using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Repository.Interfaces
{
    public interface IProductModelRepository
    {
        Task<GeneralResponse> Create(ProductModelViewMode item);
        Task<GeneralResponse> Update(ProductModelViewMode item);
        Task<GeneralResponse> Delete(ProductModelViewMode item);
        Task<ProductModelViewMode> GetItem(int id);
        Task<List<ProductModelViewMode>> GetList(int productId, string text = "");
    }
}
