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
public class UserService : GeneralServices<User>, IUserService
{
    public UserService(MicroServiceShopAuthServiceContext Context) : base(Context) { }

    public async Task<GeneralResponse<UserViewModel>> RegisterAsync(UserViewModel user)
    {
        try
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return GeneralResponse<UserViewModel>.Fail("تام کاربری را وارد کنید");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                return GeneralResponse<UserViewModel>.Fail("کلمه عبور را وارد کنید");
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                return GeneralResponse<UserViewModel>.Fail("نام را وارد کنید");
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                return GeneralResponse<UserViewModel>.Fail("نام خانوادگی را وارد کنید");
            }
            if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.PhoneNumber))
            {
                return GeneralResponse<UserViewModel>.Fail("شماره تلفن یا ایمیل باید وارد شود");
            }
            if (await _Context.Users.AnyAsync(u => u.UserName.ToLower() == user.UserName.ToLower()))
                return GeneralResponse<UserViewModel>.Fail(Message.DuplicateUserName);
          
            if (!string.IsNullOrEmpty(user.Email))
            {
                if (await _Context.Users.AnyAsync(u => u.Email.ToLower() == user.Email.ToLower()))
                    return GeneralResponse<UserViewModel>.Fail(Message.DuplicateEmail);
            }
            
            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                if (await _Context.Users.AnyAsync(u => u.PhoneNumber.ToLower() == user.PhoneNumber.ToLower()))
                    return GeneralResponse<UserViewModel>.Fail(Message.DuplicatePhoneNumber);
            }
            
            user.UserCode = await GetUserMaxCode();
            var objUser = user.ToUser();
            var res = await Add(objUser);
            if (!res.isSuccess)
            {
                return GeneralResponse<UserViewModel>.Fail(user, res.Message, res.ErrorMessage);
            }
            user.Id = objUser.Id;
            return GeneralResponse<UserViewModel>.Success(user);
        }
        catch (Exception e)
        {
            return GeneralResponse<UserViewModel>.Fail(e);
        }
    }




    private async Task<string> GetUserMaxCode()
    {
        int code = 0;
        try
        {
            if (await _Context.Users.AnyAsync())
            {
                code = await _Context.Users.MaxAsync(x => int.Parse(x.UserCode));
            }
            code = code + 1;
            return code.ToString().PadLeft(5, '0');
        }
        catch (Exception e)
        {
            code = code + 1;
            return code.ToString().PadLeft(5, '0');
        }
    }
}

