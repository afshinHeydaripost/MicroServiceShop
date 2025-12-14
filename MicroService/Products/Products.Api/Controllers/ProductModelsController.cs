using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;
using ProductService.Api.Class;


namespace ProductService.Api.Controllers;
[Authorize]
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
            await _producer.SendMessageToQueue(obj, "productCreateQueue");
            if (item.Amount != null && item.Amount > 0) {
                item.ProductModelId = res.obj.ProductModelId ?? 0;
                await _producer.SendMessageToQueue(item, "productModelAmountQueue");
            }
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
            await _producer.SendMessageToQueue(obj, "productUpdateQueue");
            if (item.Amount != null && item.Amount > 0)
            {
                await _producer.SendMessageToQueue(item, "productModelAmountQueue");
            }
        }
        return Ok(res);
    }


    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(User.GetLoginedUserId(), id);
        if (res.isSuccess)
        {
            await _producer.SendMessageToQueue(id, "productDeleteQueue");
        }
        return Ok(res);
    }
}
