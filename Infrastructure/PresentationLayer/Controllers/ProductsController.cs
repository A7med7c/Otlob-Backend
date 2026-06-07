<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> origin/Dev
using PresentationLayer.Attributes;
using SeviceAbstraction;
using Shared;
using Shared.DTOs.Product;

namespace PresentationLayer.Controllers;

[ApiController]
<<<<<<< HEAD
[Route("api/[Controller]")]
=======
[Route("api/products")]
>>>>>>> origin/Dev
public class ProductsController(IServicesManager _servicesManager) : ControllerBase
{
    [HttpGet]
    [Cash]
<<<<<<< HEAD
    public async Task<ActionResult<IEnumerable<PaginatedResult<ProductDto>>>> GetProducts([FromQuery] ProductQueryParams queryParams)
=======
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProducts([FromQuery] ProductQueryParams queryParams)
>>>>>>> origin/Dev
    {
        var products = await _servicesManager.ProductService.GetAllProductsAsync(queryParams);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
<<<<<<< HEAD
    [Authorize]
=======
>>>>>>> origin/Dev
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
