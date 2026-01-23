using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Admin.UI.Class;
public static class CookiesTools
{
    public static string GetCookiesValue(this HttpRequest httpRequest, string cookieName)
    {
        if (httpRequest.Cookies[cookieName] is null)
            return "";
        return httpRequest.Cookies[cookieName];
    }
    public static void CreateCookies(this HttpResponse httpRequest, string cookieName, string cookieValue, DateTime? expires = null)
    {
        if (expires == null)
            expires = DateTime.Now.AddMinutes(30);
        httpRequest.Cookies.Append(cookieName, cookieValue, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,  // اگر TLS فعال است
            SameSite = SameSiteMode.Strict,
            Expires = expires
        });
    }

    public static void DeleteCookies(this HttpResponse httpRequest, string cookieName)
    {
        httpRequest.Cookies.Delete(cookieName);

    }
}

