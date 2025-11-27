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
}

