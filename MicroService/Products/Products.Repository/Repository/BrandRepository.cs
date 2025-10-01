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
    public class BrandRepository : IBrandRepository
    {
        private readonly MicroServiceShopContext _context;

        public BrandRepository(MicroServiceShopContext context)
        {
            _context = context;
        }
        public Task<GeneralResponse> Create(BrandViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Delete(BrandViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<BrandViewModel> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BrandViewModel>> GetList(int userId, string text = "")
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(BrandViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
