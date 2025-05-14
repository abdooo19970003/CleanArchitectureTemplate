using CleanArc.Application.Repositories;
using MediatR;

namespace CleanArc.Application.Services.Users.Command.Add
{
    using CleanArc.Application.Interfaces;
    using CleanArc.Domain.Entities;

    public class CreateUserHandler : IRequestHandler<CreateUserRequest, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandler(IRepository<User> userRepository,
                                 IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User(request.FirstName, request.LastName, request.UserName, request.Password, request.Avatar, request.Role);
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
