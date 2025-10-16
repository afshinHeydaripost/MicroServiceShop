using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;


namespace ProductService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsServices _productService;

    public ProductsController(IProductsServices productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList(string text = "")
    {
        var list = await _productService.GetList(1, text);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var product = await _productService.GetItem(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel item)
    {
        var res = await _productService.Create(item);
        return Ok(res);
    }


    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel item)
    {

        var res = await _productService.Update(item);
        return Ok(res);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _productService.Delete(1, id);
        return Ok(res);
    }
}
