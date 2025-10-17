﻿using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Products.DataModel.Models;
using Products.Services.Interfaces;


namespace ProductService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]

public class BrandController : ControllerBase
{
    private readonly IBrandServices _service;

    public BrandController(IBrandServices service)
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

    public async Task<IActionResult> Create(BrandViewModel item)
    {
        var res = await _service.Create(item);
        return Ok(res);
    }
    [HttpPost("update")]

    public async Task<IActionResult> Update(BrandViewModel item)
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
