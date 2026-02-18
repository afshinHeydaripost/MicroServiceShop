using AuthService.DataModel.Context;
using AuthService.DataModel.Models;
using AuthService.Services.Interfaces;
using Helper;
using Helper.Base;
using Helper.VieModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Products.Services.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services;
public class UserService : GeneralServices<User>, IUserService
{
    private IUserRolService _UserRolService;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(MicroServiceShopAuthServiceContext Context, ITokenService tokenService, IPasswordHasher<User> passwordHasher, IUserRolService userRolService) : base(Context)
    {
        _UserRolService = userRolService;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }
    public async Task<GeneralResponse<UserViewModel>> LoginAsync(LoginRequestViewModel req)
    {
        try
        {
            var user = await _Context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                        .FirstOrDefaultAsync(u => u.UserName == req.Username);
            if (user == null)
                return GeneralResponse<UserViewModel>.Fail(Message.LoginFail);

            var res = _passwordHasher.VerifyHashedPassword(user, user.Password, req.Password);
            if (res == PasswordVerificationResult.Failed)
                return GeneralResponse<UserViewModel>.Fail(Message.LoginFail);

            var roles = user.UserRoles.Select(r => r.Role!.Name);
            var access = _tokenService.GenerateAccessToken(user, roles);
            if (!access.isSuccess)
            {
                return GeneralResponse<UserViewModel>.Fail(new UserViewModel(), access.Message, access.ErrorMessage);
            }
            var refresh = _tokenService.GenerateRefreshToken(user.Id, req.RememberMe, req.ipAddress);

            user.RefreshTokens.Add(refresh);
            await Save();

            return GeneralResponse<UserViewModel>.Success(new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserCode = user.UserCode,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Id = user.Id,
                Token = access.obj,
                RememberMe = req.RememberMe,
                RefreshToken = refresh.Token
            });
        }
        catch (Exception e)
        {
            return GeneralResponse<UserViewModel>.Fail(e);
        }
    }


    public async Task<GeneralResponse<UserViewModel>> RegisterAsync(UserViewModel user)
    {
        try
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                return GeneralResponse<UserViewModel>.Fail(Message.PasswordIsRequired);
            }
            if (string.IsNullOrEmpty(user.FirstName))
            {
                return GeneralResponse<UserViewModel>.Fail(Message.FirstNameIsRequired);
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                return GeneralResponse<UserViewModel>.Fail(Message.LastNameIsRequired);
            }
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                return GeneralResponse<UserViewModel>.Fail(Message.EmailOrPhoneNumberIsRequired);
            }
            if (!user.PhoneNumber.IsValidMobileNumber())
            {
                return GeneralResponse<UserViewModel>.Fail(Message.InvalidPhoneNumber);
            }
      
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
            user.UserName = user.PhoneNumber;
            var objUser = user.ToUser();
            objUser.Password = _passwordHasher.HashPassword(objUser, user.Password);
            var res = await Add(objUser);
            if (!res.isSuccess)
            {
                return GeneralResponse<UserViewModel>.Fail(user, res.Message, res.ErrorMessage);
            }
            user.Id = objUser.Id;
            var role = await _Context.Roles.FirstOrDefaultAsync(x => x.Name.ToLower() == Roles.User.ToString().ToLower());
            var userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            await _UserRolService.Add(userRole);
            user.Password = "";

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
    public async Task<GeneralResponse> RevokeRefreshTokenAsync(LoginRequestViewModel req)
    {
        try
        {
            var stored = await _Context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == req.Token);
            if (stored == null)
                return GeneralResponse.NotFound();
            stored.Revoked = true;
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async Task<GeneralResponse<UserViewModel>> RefreshTokenAsync(LoginRequestViewModel req)
    {
        try
        {
            var stored = await _Context.RefreshTokens
                .Include(rt => rt.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(rt => rt.Token == req.Token);

            if (stored == null ||
                stored.Revoked ||
                stored.ExpiresDateTime <= DateTime.UtcNow ||
                stored.ReplacedByToken != null)
                return GeneralResponse<UserViewModel>.Fail(Message.LoginFail);

            stored.Revoked = true;

            var newRefresh = _tokenService.GenerateRefreshToken(
                stored.UserId,
                stored.RememberMe,
                req.ipAddress);

            stored.ReplacedByToken = newRefresh.Token;

            _Context.RefreshTokens.Add(newRefresh);

            var roles = stored.User.UserRoles.Select(r => r.Role!.Name);

            var newAccess = _tokenService.GenerateAccessToken(stored.User, roles);
            if (!newAccess.isSuccess)
                return GeneralResponse<UserViewModel>.Fail(
                    new UserViewModel(), newAccess.Message, newAccess.ErrorMessage);

            await Save();

            return GeneralResponse<UserViewModel>.Success(new UserViewModel
            {
                Id = stored.User.Id,
                UserCode = stored.User.UserCode,
                UserName = stored.User.UserName,
                FirstName = stored.User.FirstName,
                LastName = stored.User.LastName,
                Email = stored.User.Email,
                PhoneNumber = stored.User.PhoneNumber,
                Token = newAccess.obj,
                RefreshToken = newRefresh.Token,
                RememberMe = newRefresh.RememberMe
            });
        }
        catch (Exception ex)
        {
            return GeneralResponse<UserViewModel>.Fail(ex);
        }
    }
}

