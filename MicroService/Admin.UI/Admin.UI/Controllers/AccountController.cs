using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Helper.VieModels;
using Helper;
using Admin.UI.Class;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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
    #endregion
    #region Post
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestViewModel model)
    {
		if (model==null)
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
        if (res==null || !res.isSuccess)
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

    //[HttpPost]
    //public async Task<IActionResult> LoginUser(
    //[FromBody] LoginRequestViewModel model,
    //[FromServices] IHttpClientFactory httpClientFactory)
    //{
    //    if (model == null)
    //        return Json(GeneralResponse.Fail("اطلاعات ارسالی معتبر نیست"));

    //    if (string.IsNullOrWhiteSpace(model.Username))
    //        return Json(GeneralResponse.Fail("نام کاربری را وارد کنید"));

    //    if (string.IsNullOrWhiteSpace(model.Password))
    //        return Json(GeneralResponse.Fail("کلمه عبور را وارد کنید"));

    //    var client = httpClientFactory.CreateClient("AuthService");

    //    var response = await client.PostAsJsonAsync(
    //        "authService/User/Login", model);

    //    if (!response.IsSuccessStatusCode)
    //        return Json(GeneralResponse.Fail("خطا در ارتباط با سرویس احراز هویت"));

    //    var res = await response.Content
    //        .ReadFromJsonAsync<GeneralResponse<UserViewModel>>();

    //    if (!res.isSuccess)
    //        return Json(GeneralResponse.Fail(res.Message, res.ErrorMessage));

    //    Response.CreateCookies("refreshToken", res.obj.RefreshToken, DateTime.Now.AddDays(7));

    //    // ✅ AccessToken → Front
    //    return Json(GeneralResponse.Success(new
    //    {
    //        accessToken = res.obj.Token
    //    }, "ورود با موفقیت انجام شد"));
    //}

    #endregion
}

