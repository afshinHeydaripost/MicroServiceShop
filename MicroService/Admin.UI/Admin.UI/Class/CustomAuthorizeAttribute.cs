
using Microsoft.AspNetCore.Authorization;
using Helper;


public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(params string[] roles)
    {
        var roleList = roles.ToList();

        // اگر Supervisor بود → همه دسترسی‌ها رو داره
        if (roleList.Contains(Helper.Roles.Supervisor))
        {
            roleList.Add(Helper.Roles.Admin);
            roleList.Add(Helper.Roles.User);
        }

        // اگر Admin بود → User هم داره
        if (roleList.Contains(Helper.Roles.Admin))
        {
            roleList.Add(Helper.Roles.User);
        }

        // حذف تکراری‌ها
        roleList = roleList.Distinct().ToList();

        Roles = string.Join(",", roleList);
    }
}

