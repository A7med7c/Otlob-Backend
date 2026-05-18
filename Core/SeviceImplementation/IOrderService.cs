using Shared.DTOs.Order;

namespace SeviceAbstraction
{
    public interface IOrderService
    {
        Task<ReturnedOrderDto> CreateOrderAsync(string email, OrderDto orderDto);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        Task<IEnumerable<ReturnedOrderDto>> GetAllOrdersAsync(string email);
        Task<ReturnedOrderDto> GetOrderByIdAsync(Guid Id);
    }
}
