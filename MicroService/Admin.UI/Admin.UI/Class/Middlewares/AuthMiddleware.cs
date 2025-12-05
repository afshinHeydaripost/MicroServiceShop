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

			// مسیرهایی که نیاز به احراز هویت ندارند
			var allowedPaths = new[]
			{
				"/account/login",
				"/account/loginuser"
			};

			if (allowedPaths.Contains(path))
			{
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
