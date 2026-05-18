using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.Product;
using SeviceAbstraction;
using Shared.DTOs.Identity;
using Shared.DTOs.Order;

namespace ServiceImplementation
{
    public class OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<ReturnedOrderDto> CreateOrderAsync(string email, OrderDto orderDto)
        {
            var orderAddress = mapper.Map<AddressDto, ShippingAddress>(orderDto.Address);

            var basket = await basketRepository.GetBasketById(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);

            List<OrderItem> items = [];

            var productRepository = unitOfWork.GetRepository<Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepository.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                OrderItem orderItem = CreateOrderItem(item, product);
                items.Add(orderItem);
            }

            var deliverymethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                                            .GetByIdAsync(orderDto.DeliveryMethodId)
                                            ?? throw new DeliveryNotFoundException(orderDto.DeliveryMethodId);

            var subTotal = items.Sum(i => i.Quantity * i.Price);

            var createdOrder = new Order(email, deliverymethod, orderAddress, items, subTotal);


            await unitOfWork.GetRepository<Order, Guid>().AddAsync(createdOrder);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, ReturnedOrderDto>(createdOrder);
        }

        public async Task<List<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliverymethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            List<DeliveryMethodDto> deliveryMethodDtos = [];
            foreach (var method in deliverymethods)
            {
                deliveryMethodDtos.Add(mapper.Map<DeliveryMethod, DeliveryMethodDto>(method));
            }
            return deliveryMethodDtos;
        }
        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered() { ProductId = product.Id, PictureUrl = product.PictureUrl, ProductName = product.Name },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }
    }
}
