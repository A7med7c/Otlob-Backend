using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Basket;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BasketsController(IServicesManager servicesManager) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<ActionResult<CustomerBasketDto>> GetBasket(string key)
    {
        var basket = await servicesManager.BasketService.GetBasketAsync(key);
        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateBasket(CustomerBasketDto dto)
    {
        var basket = await servicesManager.BasketService.CreateorUpdateBasketAsync(dto);
        return Ok(basket);
    }
    [HttpDelete("{key}")]
    public async Task<ActionResult<bool>> DeleteBasket(string key)
    {
        var result = await servicesManager.BasketService.DeleteBasketAsync(key);
        return Ok(result);
    }
}
