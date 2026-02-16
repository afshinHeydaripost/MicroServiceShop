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
        public void OnGet()
        {
        }

        public async Task<JsonResult> OnPostRegisterUser()
        {
            var res = new GeneralResponse<UserViewModel>();

            return new JsonResult(res);
        }
    }
}
