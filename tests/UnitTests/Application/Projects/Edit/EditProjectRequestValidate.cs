using Etn.MyLittleBoard.Application.Projects.Edit;
using FluentValidation.TestHelper;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.Edit;

public sealed class EditProjectRequestValidate
{
    private readonly EditProjectValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task EditProjectRequestValidate_ShouldBeValidWhenNameSet()
    {
        EditProjectRequest request = new(this.fixture.Create<int>()) { Name = this.fixture.Create<string>() };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task EditProjectRequestValidate_ShouldHaveErrorWhenProjectIdNotSet(int id)
    {
        EditProjectRequest request = new(id) { Name = this.fixture.Create<string>() };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorCode("GreaterThanValidator")
            .WithErrorMessage($"'Project Id' must be greater than '0'.")
            .Only();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task EditProjectRequestValidate_ShouldHaveErrorWhenNameNotSet(string? name)
    {
        EditProjectRequest request = new(this.fixture.Create<int>()) { Name = name! };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage($"'{nameof(EditProjectRequest.Name)}' must not be empty.")
            .Only();
    }

    [Fact]
    public async Task EditProjectRequestValidate_ShouldHaveErrorWhenNameTooLong()
    {
        EditProjectRequest request = new(this.fixture.Create<int>()) { Name = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength) };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(EditProjectRequest.Name)}' must be {ValidationConstants.DefaultTextLength} characters or fewer. You entered {request.Name.Length} characters.")
            .Only();
    }

    [Fact]
    public async Task EditProjectRequestValidate_CanSetDescription()
    {
        EditProjectRequest request = new(this.fixture.Create<int>()) { Description = this.fixture.Create<string>() };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public async Task EditProjectRequestValidate_ShouldHaveErrorWhenDescriptionTooLong()
    {
        EditProjectRequest request = new(this.fixture.Create<int>())
        {
            Name = this.fixture.Create<string>(),
            Description = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength)
        };

        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(EditProjectRequest.Description)}' must be {ValidationConstants.DefaultTextLength} characters or fewer. You entered {request.Description.Length} characters.")
            .Only();
    }

    [Fact]
    public async Task EditProjectRequestValidate_CanSetColor()
    {
        string color = StringHelpers.GenerateHexColor();
        EditProjectRequest request = new(this.fixture.Create<int>()) { Color = color };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Color);
    }

    [Fact]
    public async Task EditProjectRequestValidate_ShouldHaveErrorWhenNotHexColor()
    {
        EditProjectRequest request = new(this.fixture.Create<int>())
        {
            Name = this.fixture.Create<string>(),
            Color = this.fixture.Create<string>()
        };

        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Color)
            .WithErrorCode("RegularExpressionValidator")
            .WithErrorMessage($"'{nameof(EditProjectRequest.Color)}' is not in the correct format.")
            .Only();
    }
}
