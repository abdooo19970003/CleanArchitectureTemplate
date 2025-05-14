using CleanArc.Application.Repositories;
using MediatR;

namespace CleanArc.Application.Services.Users.Command.Add
{
    using CleanArc.Application.Interfaces;
    using CleanArc.Domain.Common.Enums;
    using CleanArc.Domain.Entities;

    public class CreateUserHandler : IRequestHandler<CreateUserRequest, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandler(IUserRepository userRepository,
                                 IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            User user = new User
            {
                FirstName = request.firstName,
                LastName = request.lastName,
                UserName = request.userName,
                Password = request.password,
                Role = (UserRole)request.Role
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
