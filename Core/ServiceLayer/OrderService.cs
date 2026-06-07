using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.Product;
using ServiceImplementation.Specifications.OrderModule;
using SeviceAbstraction;
using Shared.DTOs.Identity;
using Shared.DTOs.Order;

namespace ServiceImplementation
{
    public class OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<ReturnedOrderDto> CreateOrderAsync(string email, OrderDto orderDto)
        {
            var shipToAddress = orderDto.ShipToAddress ?? orderDto.Address
                ?? throw new BadRequestException(["Shipping address is required."]);
            var orderAddress = mapper.Map<AddressDto, ShippingAddress>(shipToAddress);

            var basket = await basketRepository.GetBasketById(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);
            ArgumentNullException.ThrowIfNull(basket.PaymentIntentId);

            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var orderSepcs = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId);
            var exsistingOrder = await orderRepo.GetByIdAsync(orderSepcs);

            if (exsistingOrder is not null) orderRepo.Remove(exsistingOrder);

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

            var createdOrder = new Order(email, deliverymethod, orderAddress, items, subTotal, basket.PaymentIntentId);


            await orderRepo.AddAsync(createdOrder);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, ReturnedOrderDto>(createdOrder);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliverymethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliverymethods);
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

        public async Task<IEnumerable<ReturnedOrderDto>> GetAllOrdersAsync(string email)
        {
            var specs = new OrderSpecifications(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(specs);
            return mapper.Map<IEnumerable<Order>, IEnumerable<ReturnedOrderDto>>(orders);
        }

        public async Task<ReturnedOrderDto> GetOrderByIdAsync(Guid Id)
        {
            var specs = new OrderSpecifications(Id);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specs)
                            ?? throw new OrderNotFoundException(Id);
            return mapper.Map<Order, ReturnedOrderDto>(order);
        }
    }
}
