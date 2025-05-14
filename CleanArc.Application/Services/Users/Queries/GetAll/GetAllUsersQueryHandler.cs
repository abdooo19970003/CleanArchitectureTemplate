using CleanArc.Application.Repositories;
using CleanArc.Domain.Entities;
using MediatR;

namespace CleanArc.Application.Services.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IRepository<User> _userRepository;

        public GetAllUsersQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<IEnumerable<User>> IRequestHandler<GetAllUsersQuery, IEnumerable<User>>.Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }
}
