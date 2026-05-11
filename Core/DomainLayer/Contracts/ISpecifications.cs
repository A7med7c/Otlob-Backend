using DomainLayer.Models;
using System.Linq.Expressions;

namespace DomainLayer.Contracts;

public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    Expression<Func<TEntity, object>> OrderBy { get; }
    Expression<Func<TEntity, object>> OrderByDescending { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsPaginated { get; set; }
}
