using AutoMapper;
using DomainLayer.Contracts;
using SeviceAbstraction;

namespace ServiceImplementation;

public class ServicesManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServicesManager
{
    private readonly Lazy<IProductService> _productService =
        new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
    public IProductService ProductService => _productService.Value;
}