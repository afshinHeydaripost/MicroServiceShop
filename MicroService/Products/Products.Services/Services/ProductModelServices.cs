using Helper;
using Helper.VieModels;
using Products.Services.Interfaces;

namespace Products.Services;
public class ProductModelServices : IProductModelServices
{
    public async Task<GeneralResponse> Create(ProductModelViewMode item)
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

    public async Task<GeneralResponse> Delete(int userId,int id)
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

    public Task<ProductModelViewMode> GetItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductModelViewMode>> GetList(int productId, string text = "")
    {
        throw new NotImplementedException();
    }

    public async Task<GeneralResponse> Update(ProductModelViewMode item)
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

