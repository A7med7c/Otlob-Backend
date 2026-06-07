using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Basket;

namespace PresentationLayer.Controllers;

[ApiController]
<<<<<<< HEAD
[Route("api/[Controller]")]
public class BasketsController(IServicesManager servicesManager) : ControllerBase
{
=======
[Route("api/baskets")]
public class BasketsController(IServicesManager servicesManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CustomerBasketDto>> GetBasketByQuery([FromQuery] string id)
    {
        var basket = await servicesManager.BasketService.GetBasketAsync(id);
        return Ok(basket);
    }

>>>>>>> origin/Dev
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
<<<<<<< HEAD
    [HttpDelete("{key}")]
    public async Task<ActionResult<bool>> DeleteBasket(string key)
    {
        var result = await servicesManager.BasketService.DeleteBasketAsync(key);
        return Ok(result);
=======

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketByQuery([FromQuery] string id)
    {
        await servicesManager.BasketService.DeleteBasketAsync(id);
        return NoContent();
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteBasket(string key)
    {
        await servicesManager.BasketService.DeleteBasketAsync(key);
        return NoContent();
>>>>>>> origin/Dev
    }
}
