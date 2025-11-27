using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper;
public static class Tools
{
    public static string GetRemoteIpAddress(this HttpContext item)
    {
        return item.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}

