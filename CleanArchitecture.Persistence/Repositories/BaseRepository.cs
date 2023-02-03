using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DataContext Context;

    public BaseRepository(DataContext context)
    {
        Context = context;
    }
    
    public void Create(T entity)
    {
        Context.Add(entity);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.DateCreated = DateTimeOffset.UtcNow;
        Context.Update(entity);
    }

    public Task<T> Get(Guid id, CancellationToken cancellationToken)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return Context.Set<T>().ToListAsync(cancellationToken);
    }
}