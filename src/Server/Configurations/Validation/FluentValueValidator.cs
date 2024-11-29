using FluentValidation;

namespace Etn.MyLittleBoard.Server.Configurations.Validation;

public sealed class FluentValueValidator<T, U>(T validator) where T : AbstractValidator<U>
{
    private readonly T validator = validator;

    public Func<object, string, Task<IEnumerable<string>>> Validation => async (model, property) =>
    {
        FluentValidation.Results.ValidationResult result =
            await this.validator.ValidateAsync(ValidationContext<U>.CreateWithOptions((U)model, x => x.IncludeProperties(property)));

        if (result.IsValid)
        {
            return [];
        }

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
