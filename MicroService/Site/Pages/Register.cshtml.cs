using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Site.Pages
{
    public class RegisterModel : PageModel
    {
        #region Prop
        private readonly IConfiguration _config;
        private static string _AuthServiceUrl;
        [BindProperty]
        public UserViewModel user { get; set; }
        #endregion
        public RegisterModel(IConfiguration config)
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
        public async Task<JsonResult> OnPostRegisterUser()
        {
            var res = new GeneralResponse<UserViewModel>();
            if (!ModelState.IsValid)
            {
                res.Message = ModelState.GetFirstError();
                return new JsonResult(res);
            }
            var url = _AuthServiceUrl + "authService/User/Create";
            res = url.PostData<UserViewModel, GeneralResponse<UserViewModel>>(user);
            return new JsonResult(res);
        }
        #endregion

    }
}
