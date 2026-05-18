using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Order;

namespace PresentationLayer.Controllers
{
    public class OrdersController(IServicesManager servicesManager) : ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreatedOrderDto>> CreateOrder(OrderDto dto)
        {
            var order = await servicesManager.OrderService.CreateOrderAsync(GetCurrentUserEmail(), dto);
            return Ok(order);
        }
    }
}
