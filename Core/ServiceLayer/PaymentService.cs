using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using SeviceAbstraction;
using Shared.DTOs.Basket;
using Stripe;
using Product = DomainLayer.Models.Product.Product;
namespace ServiceImplementation;

public class PaymentService(IConfiguration configuration, IBasketRepository basketRepository,
    IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
{
    public async Task<CustomerBasketDto> CreateOrUpdatePaymentAsync(string basketId)
    {
        //configure stripe
        StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
        //get basket 
        var basket = await basketRepository.GetBasketById(basketId) ??
                                        throw new BasketNotFoundException(basketId);
        //get total amount = product price + delivery cost
        var repo = unitOfWork.GetRepository<Product, int>();

        foreach (var item in basket.Items)
        {
            var product = await repo.GetByIdAsync(item.Id) ??
                                throw new ProductNotFoundException(item.Id);
            item.Price = product.Price;
        }
        ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);
        var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value) ??
                                    throw new DeliveryNotFoundException(basket.DeliveryMethodId.Value);

        basket.ShippingPrice = deliveryMethod.Price;
        var totalBsketAmount = (long)(basket.Items.Sum(item => item.Quantity * item.Price) + deliveryMethod.Price) * 100;

        //create payment intent or update 
        var paymentService = new PaymentIntentService();
        if (basket.PaymentIntentId is null)//create 
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = totalBsketAmount,
                Currency = "USD",
                PaymentMethodTypes = ["card"]
            };
            var paymentIntent = await paymentService.CreateAsync(options);
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else //update
        {
            var options = new PaymentIntentUpdateOptions() { Amount = totalBsketAmount };
            await paymentService.UpdateAsync(basket.PaymentIntentId, options);
        }
        await basketRepository.CreateBasketorUpdateAsync(basket);
        return mapper.Map<CustomerBasketDto>(basket);
    }

    public async Task UpdateOrderPaymentStatusAsync(string paymentIntentId, bool isPaymentSucceeded)
    {
        var orderRepo = unitOfWork.GetRepository<DomainLayer.Models.OrderModule.Order, Guid>();
        var order = await orderRepo.GetByIdAsync(new ServiceImplementation.Specifications.OrderModule.OrderWithPaymentIntentIdSpecifications(paymentIntentId));
        if (order is null)
            return;

        order.OrderStatus = isPaymentSucceeded ? OrderStatus.PaymentSucceded : OrderStatus.PaymentFailed;
        orderRepo.Update(order);
        await unitOfWork.SaveChangesAsync();
    }
}
