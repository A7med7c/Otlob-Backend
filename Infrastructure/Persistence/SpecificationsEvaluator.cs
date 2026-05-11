using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

static class SpecificationsEvaluator
{
    public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery,
        ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
    {
        var query = InputQuery;

        if (specifications.Criteria is not null)
            query = query.Where(specifications.Criteria);

        if (specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);

        if (specifications.OrderByDescending is not null)
            query = query.OrderByDescending(specifications.OrderByDescending);


        if (specifications.Includes is not null && specifications.Includes.Count > 0)
            query = specifications.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


        if (specifications.IsPaginated)
            query = query.Skip(specifications.Skip).Take(specifications.Take);

        return query;
    }
}
