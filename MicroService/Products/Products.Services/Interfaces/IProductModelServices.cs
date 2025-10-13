using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

public interface IProductModelServices
    {
        Task<GeneralResponse> Create(ProductModelViewMode item);
        Task<GeneralResponse> Update(ProductModelViewMode item);
        Task<GeneralResponse> Delete(int userId,int id);
        Task<ProductModelViewMode> GetItem(int id);
        Task<List<ProductModelViewMode>> GetList(int productId, string text = "");
    }

