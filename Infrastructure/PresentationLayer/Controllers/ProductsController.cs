using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Attributes;
using SeviceAbstraction;
using Shared;
using Shared.DTOs.Product;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IServicesManager _servicesManager) : ControllerBase
{
    [HttpGet]
    [Cash]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProducts([FromQuery] ProductQueryParams queryParams)
    {
        var products = await _servicesManager.ProductService.GetAllProductsAsync(queryParams);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _servicesManager.ProductService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProduct([FromForm] CreateProductDto productDto)
    {
        var id = await _servicesManager.ProductService.CreateAsync(productDto);

        return CreatedAtAction(nameof(GetProduct), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDto dto)
    {
        await _servicesManager.ProductService.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _servicesManager.ProductService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
    {
        var brands = await _servicesManager.ProductService.GetAllBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
    {
        var types = await _servicesManager.ProductService.GetAllTypesAsync();
        return Ok(types);
    }
}
