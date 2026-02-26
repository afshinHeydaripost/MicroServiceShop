using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper;
public static class Tools
{

    public static bool IsValidMobileNumber(this string mobile)
    {
        if (string.IsNullOrWhiteSpace(mobile))
            return false;

        mobile = mobile.Trim();

        string pattern = @"^(?:\+98|0)?9\d{9}$";

        return Regex.IsMatch(mobile, pattern);
    }
    public static string GetRemoteIpAddress(this HttpContext item)
    {
        return item.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
    public static int GetLoginedUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst("UserId")?.Value;
        if (userId == null || string.IsNullOrEmpty(userId))
        {
            return 0;
        }
        return int.Parse(userId);
    }

    public static string GetLoginedUserCode(this ClaimsPrincipal user)
    {
        var userCode = user.FindFirst("UserCode")?.Value;
        if (userCode == null || string.IsNullOrEmpty(userCode))
        {
            return "";
        }
        return userCode;
    }
    public static string GetLoginedUserFullName(this ClaimsPrincipal user)
    {
        var userFullName = user.FindFirst("UserFullName")?.Value;
        if (userFullName == null || string.IsNullOrEmpty(userFullName))
        {
            return "";
        }
        return userFullName;
    }
    public static GeneralResponse IsAuthenticated(this ClaimsPrincipal user)
    {
        if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            return GeneralResponse.Success();
        return GeneralResponse.Fail();
    }
}

