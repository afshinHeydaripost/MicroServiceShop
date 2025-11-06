using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Order.DataModel.Models;
using Order.Services.Interfaces;


namespace Order.Api.Controllers;
[Route("orderApi/[controller]")]
[ApiController]

public class OrderController : ControllerBase
{
    private readonly IOrderServices _service;

    //public OrderController(IOrderServices service)
    //{
    //    _service = service;
    //}

    //[HttpGet("GetListForUser")]

    //public async Task<IActionResult> GetListForUser()
    //{
    //    var list = await _service.GetListForUser(1);
    //    return Ok(list);
    //}

    //[HttpGet("{id}")]

    //public async Task<IActionResult> GetItem(int id)
    //{
    //    var item = await _service.GetItem(id);
    //    return Ok(item);
    //}

    //[HttpPost("create")]

    //public async Task<IActionResult> Create([FromBody] OrderViewModel item)
    //{
    //    var res = await _service.Create(item);
    //    return Ok(res);
    //}
    [HttpPost("update")]

    public async Task<IActionResult> Update([FromBody] OrderViewModel item)
    {

        var res = await _service.Update(item);
        return Ok(res);
    }


    [HttpPost("delete/{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(id, 1);
        return Ok(res);
    }
}
