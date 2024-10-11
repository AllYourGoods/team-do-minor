using AllYourGoods.Api.Interfaces.Model;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    IGenericRepository<T> Repository<T>() where T : class, IEntity;
}
