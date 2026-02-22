using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullSolutionSoft.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController: ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService; 
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto createProductDto)
    {
        var result = await _productService.CreateAsync(createProductDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetProductByIdsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreateProductDto dto)
    {
        var result = await _productService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _productService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent(); // 204 No Content
    }

}
