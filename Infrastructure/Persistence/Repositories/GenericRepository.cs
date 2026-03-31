using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class GenericRepository<TEntity, TKey>(ApplicationDbContext _dbContext) :
    IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly DbSet<TEntity> _dbSet = _dbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Remove(TEntity entity) => _dbSet.Remove(entity);
}
