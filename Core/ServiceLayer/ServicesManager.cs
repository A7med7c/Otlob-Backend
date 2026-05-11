using AutoMapper;
using DomainLayer.Contracts;
using SeviceAbstraction;

namespace ServiceImplementation;

public class ServicesManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketService) : IServicesManager
{
    private readonly Lazy<IProductService> _productService =
        new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
    public IProductService ProductService => _productService.Value;

    private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketService, _mapper));
    public IBasketService BasketService => _basketService.Value;
}