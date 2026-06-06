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
        public async Task<ActionResult<ReturnedOrderDto>> CreateOrder(OrderDto dto)
        {
            var order = await servicesManager.OrderService.CreateOrderAsync(GetCurrentUserEmail(), dto);
            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnedOrderDto>>> GetUserOrders()
        {
            var orders = await servicesManager.OrderService.GetAllOrdersAsync(GetCurrentUserEmail());
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReturnedOrderDto>> GetOrderById(Guid id)
        {
            var order = await servicesManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await servicesManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
