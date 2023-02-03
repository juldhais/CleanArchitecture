using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);
}