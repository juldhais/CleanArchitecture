using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
    
    public Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}