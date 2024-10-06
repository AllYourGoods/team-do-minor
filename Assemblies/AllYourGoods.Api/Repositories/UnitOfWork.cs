using AllYourGoods.Api.Data;
using AllYourGoods.Api.Interfaces.Model;
using AllYourGoods.Api.Interfaces.Repositories;
using System.Collections;

namespace AllYourGoods.Api.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Hashtable();
    }

    public IGenericRepository<T> Repository<T>() where T : class, IEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            // Create a new repository instance for the requested entity type
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type]!;
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    // Dispose pattern implementation
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
