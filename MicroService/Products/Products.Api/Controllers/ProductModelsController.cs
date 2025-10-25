using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;


namespace ProductService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductModelsController : ControllerBase
{
    private readonly IProductModelServices _service;

    public ProductModelsController(IProductModelServices service)
    {
        _service = service;
    }

    [HttpGet("GetList/{id}")]
    public async Task<IActionResult> GetList(int id)
    {
        var list = await _service.GetList(id, "");
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _service.GetItem(id);
        return Ok(item);
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ProductModelViewMode item)
    {
        var res = await _service.Create(item);
        return Ok(res);
    }


    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] ProductModelViewMode item)
    {

        var res = await _service.Update(item);
        return Ok(res);
    }


    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(1, id);
        return Ok(res);
    }
}
