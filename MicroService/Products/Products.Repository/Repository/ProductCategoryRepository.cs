using Helper;
using Helper.VieModels;
using Products.Repository.Context;
using Products.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly MicroServiceShopContext _context;

        public ProductCategoryRepository(MicroServiceShopContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> Create(ProductCategoryViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Delete(ProductCategoryViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategoryViewModel> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductCategoryViewModel>> GetList(int userId, string text = "")
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(ProductCategoryViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
