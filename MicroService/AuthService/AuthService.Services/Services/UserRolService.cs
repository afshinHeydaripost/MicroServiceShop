using AuthService.DataModel.Context;
using AuthService.DataModel.Models;
using AuthService.Services.Interfaces;
using AuthService.Services.Tools;
using Helper;
using Helper.Base;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Products.Services.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services;
public class UserRolService : GeneralServices<UserRole>, IUserRolService
{
    public UserRolService(MicroServiceShopAuthServiceContext Context) : base(Context) { }

    public async Task<List<UserRoleViewModel>> GetListForUser(int userId)
    {
        var query = _Context.Roles.AsQueryable();
        return await query.Select(x => new UserRoleViewModel()
        {
            UserId = userId,
            RoleId = x.Id,
            UserRoles = x.UserRoles.Any(z => z.UserId == userId && z.RoleId == x.Id),
            RoleName = x.Name
        }).ToListAsync();
    }

    public async Task<GeneralResponse> RegisterUserRoles(UserRoleViewModel item)
    {
        if (item.UserId == 0 || item.RolesIds == null || item.RolesIds.Length <= 0)
        {
            return GeneralResponse.Fail(Message.InvalidData);
        }
        try
        {
            var rools = await GetQuery().Where(x => x.UserId == item.UserId).ToListAsync();
            if (rools != null)
            {
                _Context.RemoveRange(rools);
            }
            var roles = _Context.Roles.Where(x => item.RolesIds.Contains(x.Id)).ToList().RemoveLowerRoles();
            if (roles == null || roles.Count <= 0)
            {
                return GeneralResponse.Fail(Message.InvalidData);
            }
            List<UserRole> userRoles = new List<UserRole>();
            foreach (var role in roles)
            {
                userRoles.Add(new UserRole()
                {
                    RoleId = role.Id,
                    UserId = item.UserId,
                });
            }
            await _Context.UserRoles.AddRangeAsync(userRoles);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

