using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using ProductService.Api.Class;


namespace ProductService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductModelsController : ControllerBase
{
    private readonly IProductModelServices _service;
    private readonly IRabbitMQ _producer;
    public ProductModelsController(IProductModelServices service, IRabbitMQ producer)
    {
        _service = service;
        _producer = producer;
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
        if (res.isSuccess)
        {
            var obj = await _service.GetItemInfo(res.obj.ProductModelId ?? 0);
            await _producer.SendProductMessage(obj, "productCreateQueue");
        }
        return Ok(res);
    }


    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] ProductModelViewMode item)
    {

        var res = await _service.Update(item);
        if (res.isSuccess)
        {
            var obj = await _service.GetItemInfo(item.ProductModelId ?? 0);
            await _producer.SendProductMessage(obj, "productUpdateQueue");
        }
        return Ok(res);
    }


    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(1, id);
        return Ok(res);
    }
}
