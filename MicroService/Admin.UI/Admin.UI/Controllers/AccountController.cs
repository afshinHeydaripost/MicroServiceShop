using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Helper.VieModels;
using Helper;
using Admin.UI.Class;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System;

namespace Admin.UI.Controllers;

public class AccountController : Controller
{
    private readonly IConfiguration _config;
    private static string _AuthServiceUrl;
    public AccountController(IConfiguration config)
    {
        _config = config;
        _AuthServiceUrl = _config.GetValue<string>("ApiUrl:AuthService").ToString();
    }
    #region Get

    public IActionResult Login()
    {
        return View();
    }
    [Authorize(Roles = Roles.Supervisor)]
    public async Task<IActionResult> UserAccess()
    {
        var lstUsers = await _AuthServiceUrl.SendAuthHeaderAndGetData<List<UserViewModel>>("authService/User/GetUserList", Request.GetCookiesValue("userToken"));
        return View(lstUsers);
    }

    [Authorize(Roles = Roles.Supervisor)]
    public async Task<IActionResult> GetUserRoles(int? userId = null)
    {
        var lstUserRole = await _AuthServiceUrl.SendAuthHeaderAndGetData<List<UserRoleViewModel>>($"authService/UserRole/GetUserRoles?userId={userId}", Request.GetCookiesValue("userToken"));
        return PartialView("_UserRoles", lstUserRole);
    }
    #endregion
    #region Post
    [HttpPost]
    [Authorize(Roles = Roles.Supervisor)]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterUserRoles([FromBody] UserRoleViewModel model)
    {
        var url = _AuthServiceUrl + "authService/UserRole/RegisterUserRoles";
        var res = url.SendAuthHeaderAndPostData<UserRoleViewModel, GeneralResponse>(model, Request.GetCookiesValue("userToken"));
        if (res == null || !res.isSuccess)
        {
            return Json(GeneralResponse.Fail(res.Message, res.ErrorMessage));
        }
        return Json(GeneralResponse.Success());
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestViewModel model)
    {
        if (model == null)
        {
            return Json(GeneralResponse.Fail("اطلاعات ارسالی معتبر نمیباشد"));
        }
        if (string.IsNullOrEmpty(model.Username))
        {
            return Json(GeneralResponse.Fail("نام کاربری را وارد کنید"));
        }
        if (string.IsNullOrEmpty(model.Password))
        {
            return Json(GeneralResponse.Fail("کلمه عیور را وارد کنید"));
        }
        var url = _AuthServiceUrl + "authService/User/Login";
        var res = url.PostData<LoginRequestViewModel, GeneralResponse<UserViewModel>>(model);
        if (res == null || !res.isSuccess)
        {
            return Json(GeneralResponse.Fail(res.Message, res.ErrorMessage));
        }

        // زمان expire Cookie بر اساس RememberMe
        var accessExpire = model.RememberMe
            ? DateTime.UtcNow.AddDays(2)
            : DateTime.UtcNow.AddMinutes(15);

        var refreshExpire = model.RememberMe
            ? DateTime.UtcNow.AddDays(2)
            : DateTime.UtcNow.AddDays(1);

        Response.CreateCookies("userToken", res.obj.Token, accessExpire);
        Response.CreateCookies("refreshToken", res.obj.RefreshToken, refreshExpire);
        return Json(GeneralResponse.Success());
    }

    #endregion
}

