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
    public class ProductModelRepository : IProductModelRepository
    {
        private readonly MicroServiceShopContext _context;

        public ProductModelRepository(MicroServiceShopContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> Create(ProductModelViewMode item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Delete(ProductModelViewMode item)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModelViewMode> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductModelViewMode>> GetList(int productId, string text = "")
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(ProductModelViewMode item)
        {
            throw new NotImplementedException();
        }
    }
}
