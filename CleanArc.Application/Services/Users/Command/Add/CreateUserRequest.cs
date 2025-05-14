using MediatR;

namespace CleanArc.Application.Services.Users.Command.Add
{
    using CleanArc.Domain.Common.Enums;
    using CleanArc.Domain.Entities;
    public class CreateUserRequest : IRequest<User>
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }
}