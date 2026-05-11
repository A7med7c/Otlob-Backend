using DomainLayer.Models;

namespace DomainLayer.Contracts;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);

    #region Specifications
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
    Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
    Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
    #endregion
}
