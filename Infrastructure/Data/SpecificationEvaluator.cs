using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator
{
    // This class can be used to evaluate specifications against a queryable source.
    // It can include methods to apply criteria, sorting, pagination, etc. based on the specifications.
    // For now, it is left empty as a placeholder for future implementation.

    public static IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecifications<T> spec)
    {
        // Apply the criteria from the specification to the input query
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);
        if (spec.OrderByDescending != null) query = query.OrderByDescending(spec.OrderByDescending);
        if(spec.IsDistinct) query = query.Distinct();

        return query;
    }

public static IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecifications<T, TResult> spec)
{
    // ✅ Apply filtering criteria if available
    if (spec.Criteria != null)
    {
        query = query.Where(spec.Criteria);
    }

    // ✅ Apply ordering if specified
    if (spec.OrderBy != null)
        query = query.OrderBy(spec.OrderBy);

    if (spec.OrderByDescending != null)
        query = query.OrderByDescending(spec.OrderByDescending);

    // ✅ Initialize selectQuery with type casting if possible
    IQueryable<TResult> selectQuery = query as IQueryable<TResult>;

    // ✅ Apply projection if a selector is specified
    if (spec.Select != null)
    {
        selectQuery = query.Select(spec.Select);
    }
    
    if(spec.IsDistinct) selectQuery = selectQuery.Distinct();

    // ✅ Return the projected query or fallback to casting
        return selectQuery ?? query.Cast<TResult>();
}

}
