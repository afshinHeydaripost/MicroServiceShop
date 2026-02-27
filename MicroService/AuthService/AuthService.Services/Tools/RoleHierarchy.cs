using AuthService.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.Tools;
public static class RoleHierarchy
{
    private static readonly Dictionary<string, string[]> _hierarchy =
        new()
        {
            { Helper.Roles.Supervisor, new[] { Helper.Roles.Admin, Helper.Roles.User } },
            { Helper.Roles.Admin, new[] { Helper.Roles.User } }
        };

    public static IEnumerable<string> GetAllRoles(IEnumerable<string> roles)
    {
        var result = new HashSet<string>(roles);

        foreach (var role in roles)
        {
            if (_hierarchy.ContainsKey(role))
            {
                foreach (var childRole in _hierarchy[role])
                {
                    result.Add(childRole);
                }
            }
        }

        return result;
    }
    public static List<Role> RemoveLowerRoles(this List<Role> roles)
    {
        var roleList = roles.ToList();

        foreach (var role in roles)
        {
            if (_hierarchy.ContainsKey(role.Name))
            {
                var lowerRoles = _hierarchy[role.Name];

                roleList.RemoveAll(r => lowerRoles.Contains(r.Name));
            }
        }

        return roleList;
    }
}

