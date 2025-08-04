using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecifications<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool IsDistinct { get; }
}

public interface ISpecifications<T, TResult> : ISpecifications<T>
{
    Expression<Func<T,TResult>>? Select { get; }
}
