namespace DomainLayer.Contracts;

public interface ICashRepository
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, string value, TimeSpan lifeTime);
}
