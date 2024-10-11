using AllYourGoods.Api.Data;
using AllYourGoods.Api.Interfaces.Model;
using AllYourGoods.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AllYourGoods.Api.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private readonly ApplicationContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public T Add(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbSet.Add(entity);
        return entity;
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
    {
        return await(filter != null ? _dbSet.CountAsync(filter) : _dbSet.CountAsync());
    }

    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbSet.Remove(entity);
    }

    public async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = ApplyIncludes(query, includes);

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }
        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        query = ApplyIncludes(query, includes);
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbSet.Update(entity);
    }

    // Helper method to apply dynamic include expressions for related entities
    private IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
    {

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
