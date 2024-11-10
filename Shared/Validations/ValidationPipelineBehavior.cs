using FluentValidation;
using MediatR;

namespace Shared.Validations;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    private IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var validationErrors =
                _validators.Select(v =>
                        v.Validate(request))
                    .SelectMany(r => r.Errors)
                    .Select(x => new ValidationError
                    {
                        Field = x.PropertyName,
                        ErrorMessage = x.ErrorMessage,
                        ErrorMessageCode = x.ErrorCode
                    })
                    .ToList();
            if (validationErrors.Count > 0)
            {
                var exception = new ValidationException
                {
                    Errors = validationErrors
                };
                throw exception;
            }
        }
        return next();
    }
}