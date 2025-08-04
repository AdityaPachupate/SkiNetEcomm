using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
        return entity;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool Exists(int id)
    {
        return context.Set<T>().Any(x=>x.Id== id);
    }

    private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
    {
        return SpecificationEvaluator.GetQuery(context.Set<T>().AsQueryable(), spec);
    }

    public async Task<T?> GetEntityWithSpec(ISpecifications<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }
}
