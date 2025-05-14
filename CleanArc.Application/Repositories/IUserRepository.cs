using CleanArc.Domain.Entities;

namespace CleanArc.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsUsernameAvailable(string username, CancellationToken cancellationToken = default);
    }
}
