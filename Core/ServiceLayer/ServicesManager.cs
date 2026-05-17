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
       new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration));


    public IProductService ProductService => _productService.Value;
    public IBasketService BasketService => _basketService.Value;
    public IAuthenticationService AuthenticationService => _authService.Value;
}