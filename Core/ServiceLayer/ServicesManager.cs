using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdetityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SeviceAbstraction;

namespace ServiceImplementation;

public class ServicesManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRpository,
    UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IServicesManager
{
    private readonly Lazy<IProductService> _productService =
        new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
    private readonly Lazy<IBasketService> _basketService =
        new Lazy<IBasketService>(() => new BasketService(_basketRpository, _mapper));
    private readonly Lazy<IAuthenticationService> _authService =
       new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration, _mapper));
    private readonly Lazy<IOrderService> _orderService =
          new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRpository, _unitOfWork));


    public IProductService ProductService => _productService.Value;
    public IBasketService BasketService => _basketService.Value;
    public IAuthenticationService AuthenticationService => _authService.Value;

    public IOrderService OrderService => _orderService.Value;
}