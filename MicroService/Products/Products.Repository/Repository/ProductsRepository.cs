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
    public class ProductsRepository : IProductsRepository
    {
        private readonly MicroServiceShopContext _context;

        public ProductsRepository(MicroServiceShopContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> Create(ProductViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Delete(ProductViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetList(int userId, string text = "")
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(ProductViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
