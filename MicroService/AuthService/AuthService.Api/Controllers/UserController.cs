using AuthService.Services;
using AuthService.Services.Interfaces;
using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
