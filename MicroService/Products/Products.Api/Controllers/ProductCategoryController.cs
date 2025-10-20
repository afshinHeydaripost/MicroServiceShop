﻿using Helper.VieModels;
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

    [HttpGet("GetList")]

    public async Task<IActionResult> GetList(string text = "")
    {
        var list = await _service.GetList(1, text);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _service.GetItem(id);
        return Ok(item);
    }


    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]  ProductCategoryViewModel item)
    {
        var res = await _service.Create(item);
        return Ok(res);
    }


    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody]  ProductCategoryViewModel item)
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
