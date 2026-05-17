using Shared.DTOs.Order;

namespace SeviceAbstraction
{
    public interface IOrderService
    {
        Task<CreatedOrderDto> CreateOrderAsync(string email, OrderDto orderDto);

    }
}
