using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Helper.VieModels;
using Helper;
using Admin.UI.Class;

namespace Admin.UI.Controllers;

public class AccountController : Controller
{
    private readonly IConfiguration _config;
    private static string _AuthServiceUrl;
    public AccountController(IConfiguration config)
    {
        _config = config;
        _AuthServiceUrl = _config.GetValue<string>("ApiUrll:AuthService").ToString();
    }
    #region Get

    public IActionResult Login()
    {
        return View();
    }
    #endregion
    #region Post
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestViewModel model)
    {
        if (string.IsNullOrEmpty(model.Username))
        {
            return Json(GeneralResponse.Fail("نام کاربری را وارد کنید"));
        }
        if (string.IsNullOrEmpty(model.Password))
        {
            return Json(GeneralResponse.Fail("کلمه عیور را وارد کنید"));
        }
        var url = _AuthServiceUrl + "authService/User/Login";
        var res = await url.PostAsync<GeneralResponse<UserViewModel>>(model);
        return Json(GeneralResponse.Fail());
    }
    #endregion
}

