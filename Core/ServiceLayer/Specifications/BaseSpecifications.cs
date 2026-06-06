using DomainLayer.Contracts;
using DomainLayer.Models;
using System.Linq.Expressions;

namespace ServiceImplementation.Specifications;

internal abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
{
    #region Filtration
    protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpressions)
    {
        Criteria = CriteriaExpressions;
    }

    public Expression<Func<TEntity, bool>> Criteria { get; private set; }

    #endregion


    #region Includes
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

    public void AddIncludes(Expression<Func<TEntity, object>> IncludesExpression)
    {
        Includes.Add(IncludesExpression);
    }
    #endregion


    #region Sorting
    public Expression<Func<TEntity, object>> OrderBy { get; private set; }

    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExcp) => OrderBy = orderByExcp;
    protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExcp) => OrderByDescending = orderByDescExcp;
    #endregion

    #region Pagination
    public int Take { get; set; }
    public int Skip { get; set; }

    public bool IsPaginated { get; set; }
    protected void ApplyPagination(int PageSize, int PageIndex)
    {
        IsPaginated = true;
        Take = PageSize;
        Skip = (PageIndex - 1) * PageSize;
    }
    #endregion

}
