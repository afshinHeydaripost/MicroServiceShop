using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Class
{
    public class CustomeController : Controller
    {
        private readonly IConfiguration _config;
        private static string _AuthServiceUrl;
        public CustomeController(IConfiguration config)
        {
            _config = config;
            _AuthServiceUrl = _config.GetValue<string>("ApiUrll:AuthService").ToString();
        }

    }
}
