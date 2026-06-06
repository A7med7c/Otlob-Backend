namespace SeviceAbstraction;

public interface IServicesManager
{
    public IProductService ProductService { get; }
    public IBasketService BasketService { get; }
    public IAuthenticationService AuthenticationService { get; }
    public IOrderService OrderService { get; }
}
