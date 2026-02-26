using AuthService.DataModel.Models;
using Helper;
using Helper.Base;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.Interfaces
{
    public interface IUserService : IGeneralServices<User>
    {
        Task<GeneralResponse<UserViewModel>> RegisterAsync(UserViewModel user);
        Task<GeneralResponse<UserViewModel>> LoginAsync(LoginRequestViewModel req);
        Task<GeneralResponse<UserViewModel>> RefreshTokenAsync(LoginRequestViewModel req);
        Task<GeneralResponse> RevokeRefreshTokenAsync(LoginRequestViewModel req);
        Task<UserViewModel> GetUserInfo(int userId);
        Task<List<UserViewModel>> GetUserList(string text="");
        Task<GeneralResponse> UserIsValid(int userId);

    }
}
