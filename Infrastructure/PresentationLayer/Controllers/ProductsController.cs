using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductsController(IServicesManager _servicesManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _servicesManager.ProductService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _servicesManager.ProductService.GetProductByIdAsync(id);
        return Ok(product);
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
