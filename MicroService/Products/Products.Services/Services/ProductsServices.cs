using Helper;
using Helper.VieModels;
using Products.Services.Interfaces;

namespace Products.Services;

public class ProductsServices : IProductsServices
{
    public async Task<GeneralResponse> Create(ProductViewModel item)
    {
        try
        {

            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<GeneralResponse> Delete(ProductViewModel item)
    {
        try
        {

            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public Task<ProductViewModel> GetItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductViewModel>> GetList(int userId, string text = "")
    {
        throw new NotImplementedException();
    }

    public async Task<GeneralResponse> Update(ProductViewModel item)
    {
        try
        {

            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

