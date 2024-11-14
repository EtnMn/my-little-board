using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Etn.MyLittleBoard.Application.Configurations;
internal sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));

        ValidationFailure[] errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        if (errors.Length != 0)
        {
            throw new ValidationException(errors);
        }

        TResponse? response = await next();

        return response;
    }
}

