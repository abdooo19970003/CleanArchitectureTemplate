using CleanArc.Application.Exceptions;
using CleanArc.Application.Repositories;
using CleanArc.Domain.Entities;
using CleanArc.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArc.Infrastructure.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsUsernameAvailable(string username, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username Can't be Empty");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken: cancellationToken);
            if (user == null)
                return true;
            return false;
        }

        public async Task<User?> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (await IsUsernameAvailable(user.UserName, cancellationToken))
            {
                await _context.Users.AddAsync(user, cancellationToken);
                return user;
            }
            else
            {
                throw new ConflictException("Username already exists");
            }
        }
    }
}
