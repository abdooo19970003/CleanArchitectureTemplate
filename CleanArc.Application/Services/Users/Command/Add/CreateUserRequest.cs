using MediatR;

namespace CleanArc.Application.Services.Users.Command.Add
{
    using CleanArc.Domain.Common.Enums;
    using CleanArc.Domain.Entities;
    public record CreateUserRequest(string firstName, string lastName, string userName, string password, UserRole Role = UserRole.User) : IRequest<User>;
}