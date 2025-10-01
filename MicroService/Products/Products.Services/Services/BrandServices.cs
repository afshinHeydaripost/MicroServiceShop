using Helper;
using Helper.VieModels;
using Products.Services.Interfaces;

namespace Products.Services;
public class BrandServices : IBrandServices
{
    public Task<GeneralResponse> Create(BrandViewModel item)
    {
        try
        {

        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
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

