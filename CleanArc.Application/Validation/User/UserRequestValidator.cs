using CleanArc.Application.DTOs;
using CleanArc.Application.Services.Users.Command.Add;
using FluentValidation;

namespace CleanArc.Application.Validation.User
{
    public class UserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.userName)
                .NotEmpty()
                .WithMessage("UserName is required.")
                .MinimumLength(3)
                .WithMessage("UserName must be at least 3 characters long.")
                .MaximumLength(20)
                .WithMessage("UserName must not exceed 20 characters.");

            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(20)
                .WithMessage("Password must not exceed 20 characters.")
                .Matches(@"[a-z]+").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[0-9]+").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]+").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.firstName)
                .NotEmpty()
                .WithMessage("FirstName is required.");


            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Role must be a valid Role value.");

        }
    }
}
