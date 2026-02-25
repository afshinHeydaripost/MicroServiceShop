using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Site.Pages
{
    public class LoginModel : PageModel
    {
        #region Prop
        private readonly IConfiguration _config;
        private static string _AuthServiceUrl;
        [BindProperty]
        public LoginRequestViewModel loginRequest{ get; set; }
        #endregion

        public LoginModel(IConfiguration config)
        {
            _config = config;
            _AuthServiceUrl = _config.GetValue<string>("ApiUrl:AuthService").ToString();
        }
        #region Get
        public void OnGet()
        {

        }
        #endregion

        #region Post
        public async Task<JsonResult> OnPostLogin()
        {
            var res = new GeneralResponse();
            if (!ModelState.IsValid)
            {
                return new JsonResult(GeneralResponse.Fail(ModelState.GetFirstError()));
            }
            var url = _AuthServiceUrl + "authService/User/Login";
            var result = url.PostData<LoginRequestViewModel, GeneralResponse<UserViewModel>>(loginRequest);
            if (result == null || !result.isSuccess)
            {
                return new JsonResult(GeneralResponse.Fail(result.Message, result.ErrorMessage));
            }

            // زمان expire Cookie بر اساس RememberMe
            var accessExpire = loginRequest.RememberMe
                ? DateTime.UtcNow.AddDays(2)
                : DateTime.UtcNow.AddMinutes(15);

            var refreshExpire = loginRequest.RememberMe
                ? DateTime.UtcNow.AddDays(2)
                : DateTime.UtcNow.AddDays(1);

            Response.CreateCookies("userToken", result.obj.Token, accessExpire);
            Response.CreateCookies("refreshToken", result.obj.RefreshToken, refreshExpire);
            return new JsonResult(GeneralResponse.Success());
        }
        #endregion
    }
}
