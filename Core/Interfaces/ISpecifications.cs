using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecifications<T>
{
    Expression<Func<T,bool>> Criteria {get;}
}
