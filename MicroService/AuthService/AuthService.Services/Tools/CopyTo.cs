using AuthService.DataModel.Models;
using Helper.VieModels;
namespace Products.Services.Tools;

internal static class CopyTo
{
    internal static User ToUser(this UserViewModel x)
    {
        return new User()
        {
            UserCode = x.UserCode,
            CreateDateTime=DateTime.Now,
            Email=x.Email,
            EmailConfirmed=false,
            FirstName=x.FirstName,
            LastName=x.LastName,
            Password=x.Password,
            PhoneNumber=x.PhoneNumber,
            PhoneNumberConfirmed=false,
            UserName=x.UserName,
        };
    }

}
