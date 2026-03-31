using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
namespace Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext _dbContext) : IUnitOfWork
{
    // create dic to add repos that created before.
    private readonly Dictionary<string, object> _repositories = [];
    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        // get type name 
        var type = typeof(TEntity).Name;
        // check existance  
        if (_repositories.TryGetValue(type, out object? value))
            return (IGenericRepository<TEntity, TKey>)_repositories[type];
        else
        {
            // create new repository.
            var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[type] = repository;
            return repository;
        }
    }
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
