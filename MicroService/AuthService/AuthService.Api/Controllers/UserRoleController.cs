using AuthService.Services;
using AuthService.Services.Interfaces;
using Helper.VieModels;
using Helper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Api.Controllers
{
    [Authorize(Roles = Roles.Supervisor)]
    [ApiController]
    [Route("authService/[controller]")]
    public class UserRoleController : Controller
    {
        private readonly IUserRolService _service;
        public UserRoleController(IUserRolService service)
        {
            _service = service;
        }
        [HttpGet("GetUserRoles")]

        public async Task<IActionResult> GetUserRoles(int? userId = null)
        {
            if (userId == null || userId == 0)
            {
                userId = User.GetLoginedUserId();
            }
            var lstUserRoles = await _service.GetListForUser(userId.Value);
            return Ok(lstUserRoles);
        }
    }
}
