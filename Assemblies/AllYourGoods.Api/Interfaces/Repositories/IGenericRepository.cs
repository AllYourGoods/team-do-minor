using AllYourGoods.Api.Interfaces.Model;
using System.Linq.Expressions;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<T, object>>[] includes);

    Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

    T Add(T entity);

    void Update(T entity);

    void Delete(T entity);
}