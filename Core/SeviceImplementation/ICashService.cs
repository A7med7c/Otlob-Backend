namespace SeviceAbstraction;

public interface ICashService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, object value, TimeSpan lifeTime);
}
