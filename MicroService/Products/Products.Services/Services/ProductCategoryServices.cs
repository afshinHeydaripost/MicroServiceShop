using Helper;
using Helper.VieModels;
using Products.Services.Interfaces;

namespace Products.Services;
public class ProductCategoryServices : IProductCategoryServices
{
    public async  Task<GeneralResponse> Create(ProductCategoryViewModel item)
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

    public async Task<GeneralResponse> Delete(ProductCategoryViewModel item)
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

    public Task<ProductCategoryViewModel> GetItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductCategoryViewModel>> GetList(int userId, string text = "")
    {
        throw new NotImplementedException();
    }

    public async Task<GeneralResponse> Update(ProductCategoryViewModel item)
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

