namespace Admin.UI.Class.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = "";
            // اگر مسیر Login است → چک نکن
            var path = context.Request.Path.ToString().ToLower();
            if (path.Contains("/account/login"))
            {
                //token = context.Request.GetCookiesValue("userToken");
                //if (!string.IsNullOrEmpty(token))
                //{
                //    context.Response.Redirect("/");
                //    return;
                //}
                await _next(context);
                return;
            }

            // بررسی وجود توکن
            token = context.Request.GetCookiesValue("userToken");
            if (string.IsNullOrEmpty(token))
            {
                context.Response.Redirect("/account/Login");
                return;
            }

            await _next(context);
        }
    }

}
