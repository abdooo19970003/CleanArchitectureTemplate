using MediatR;

namespace CleanArc.Application.Services.Users.Queries.GetAll
{
    using CleanArc.Domain.Entities;
    public record GetAllUsersQuery : IRequest<IEnumerable<User>>;

}
