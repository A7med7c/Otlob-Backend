using Shared.DTOs.Order;

namespace SeviceAbstraction
{
    public interface IOrderService
    {
        Task<ReturnedOrderDto> CreateOrderAsync(string email, OrderDto orderDto);
        Task<List<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
