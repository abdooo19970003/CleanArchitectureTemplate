using FluentValidation;
using MediatR;

namespace CleanArc.Application.Validation
{
    public class ValidationPipline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPipline(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            var context = new ValidationContext<TRequest>(request);
            var errors = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
            return await next(cancellationToken);
        }
    }
}
