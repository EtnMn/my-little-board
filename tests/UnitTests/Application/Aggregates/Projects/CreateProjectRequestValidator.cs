using Etn.MyLittleBoard.Application.Projects.Create;
using FluentValidation.TestHelper;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects;

public sealed class CreateProjectRequestValidator
{
    private readonly CreateProjectValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task CreateProjectValidator_ShouldHaveValidWhenNameSet()
    {
        CreateProjectRequest request = new(this.fixture.Create<string>());
        TestValidationResult<CreateProjectRequest> result = await this.validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [MemberData(nameof(StringValidationExceptionData))]
    public async Task CreateProjectValidator_ShouldHaveErrorWhenNameNotSet(string? name)
    {
        CreateProjectRequest request = new(name!);
        TestValidationResult<CreateProjectRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage($"'{nameof(CreateProjectRequest.Name)}' must not be empty.")
            .Only();
    }

    [Fact]
    public async Task CreateProjectValidator_ShouldHaveErrorWhenNameTooLong()
    {
        CreateProjectRequest request = new(new string('x', ValidationConstants.DefaultNameLength + 1));
        TestValidationResult<CreateProjectRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(CreateProjectRequest.Name)}' must be {ValidationConstants.DefaultNameLength} characters or fewer. You entered {ValidationConstants.DefaultNameLength + 1} characters.")
            .Only();
    }

    public static TheoryData<string?> StringValidationExceptionData => new()
    {
        { null },
        { string.Empty },
        { " " },
    };
}
