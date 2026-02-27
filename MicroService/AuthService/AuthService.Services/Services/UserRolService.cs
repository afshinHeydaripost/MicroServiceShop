using AuthService.DataModel.Context;
using AuthService.DataModel.Models;
using AuthService.Services.Interfaces;
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
            UserRoles = x.UserRoles.Any(z => z.UserId==userId &&  z.RoleId == x.Id),
            RoleName = x.Name
        }).ToListAsync();
    }
}

