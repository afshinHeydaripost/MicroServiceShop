using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;


namespace ProductService.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]

public class DiscountController : ControllerBase
{
    private readonly IDiscountServices _service;

    public DiscountController(IDiscountServices service)
    {
        _service = service;
    }
    [HttpGet("GetAll")]
    [AllowAnonymous]

    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetActiveList();
        return Ok(list);
    }

    [HttpGet("GetList")]

    public async Task<IActionResult> GetList(bool showAll = true, string text = "")
    {
        var list = await _service.GetList(User.GetLoginedUserId(), showAll, text);
        return Ok(list);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _service.GetItem(id);
        return Ok(item);
    }

    [HttpPost("create")]

    public async Task<IActionResult> Create([FromBody] DiscountViewModel item)
    {
        var res = await _service.Create(item);
        return Ok(res);
    }
    [HttpPost("update")]

    public async Task<IActionResult> Update([FromBody] DiscountViewModel item)
    {

        var res = await _service.Update(item);
        return Ok(res);
    }


    [HttpPost("delete/{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(id, User.GetLoginedUserId());
        return Ok(res);
    }
}
