using Helper;
using Helper.VieModels;
using Products.Services.Interfaces;

namespace Products.Services;
public class BrandServices : IBrandServices
{
    public async  Task<GeneralResponse> Create(BrandViewModel item)
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

    public async  Task<GeneralResponse> Delete(BrandViewModel item)
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

    public async  Task<BrandViewModel> GetItem(int id)
    {
        throw new NotImplementedException();
    }

    public async  Task<List<BrandViewModel>> GetList(int userId, string text = "")
    {
        throw new NotImplementedException();
    }

    public async  Task<GeneralResponse> Update(BrandViewModel item)
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

