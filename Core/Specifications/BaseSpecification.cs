using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecifications<T>
{
    
    public Expression<Func<T, bool>> Criteria => criteria;

}
