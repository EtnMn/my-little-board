using Ardalis.Result.FluentValidation;
using FluentValidation.Results;

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

        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));

        ValidationResult[] errors = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .ToArray();

        if (errors.Length != 0)
        {
            return (TResponse)typeof(Result<>).MakeGenericType(typeof(TResponse).GetGenericArguments())
                .GetMethod(nameof(Result.Invalid), [typeof(ValidationError[])])!
                .Invoke(null, errors.Select(e => e.AsErrors().ToArray()).ToArray())!;
        }

        TResponse? response = await next();

        return response;
    }
}

