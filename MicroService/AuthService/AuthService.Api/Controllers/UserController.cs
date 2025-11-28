using AuthService.Services;
using AuthService.Services.Interfaces;
using Helper.VieModels;
using Helper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("authService/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserViewModel item)
        {
            var res = await _service.RegisterAsync(item);
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestViewModel item)
        {
            item.ipAddress = HttpContext.GetRemoteIpAddress();
            var res = await _service.LoginAsync(item);
            return Ok(res);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] LoginRequestViewModel item)
        {
            item.ipAddress = HttpContext.GetRemoteIpAddress();
            var res = await _service.RefreshTokenAsync(item);
            return Ok(res);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] LoginRequestViewModel item)
        {
            item.ipAddress = HttpContext.GetRemoteIpAddress();
            var res = await _service.RevokeRefreshTokenAsync(item);
            return Ok(res);
        }
    }
}
