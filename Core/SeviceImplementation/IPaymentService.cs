using Shared.DTOs.Basket;

namespace SeviceAbstraction;

public interface IPaymentService
{
    Task<CustomerBasketDto> CreateOrUpdatePaymentAsync(string basketId);
    Task UpdateOrderPaymentStatusAsync(string paymentIntentId, bool isPaymentSucceeded);
}
