using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;


namespace ProductService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryServices _service;

    public ProductCategoryController(IProductCategoryServices service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetList(string text = "")
    {
        var list = await _service.GetList(1, text);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var product = await _service.GetItem(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCategoryViewModel item)
    {
        var res = await _service.Create(item);
        return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ProductCategoryViewModel item)
    {

        var res = await _service.Update(item);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _service.Delete(1, id);
        return Ok(res);
    }
}
