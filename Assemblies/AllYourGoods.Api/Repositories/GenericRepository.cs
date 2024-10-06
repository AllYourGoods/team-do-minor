using AllYourGoods.Api.Data;
using AllYourGoods.Api.Interfaces.Model;
using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Models;
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

    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbSet.Remove(entity);
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
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

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        query = ApplyIncludes(query, includes);
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<PaginatedList<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null) query = query.Where(filter);
        query = ApplyIncludes(query, includes);
        query = orderBy != null ? orderBy(query) : query.OrderBy(e => e.Id);
        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, totalCount, pageNumber, pageSize);
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
