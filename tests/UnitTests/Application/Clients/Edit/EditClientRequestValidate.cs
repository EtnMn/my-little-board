using Etn.MyLittleBoard.Application.Clients.Edit;
using Etn.MyLittleBoard.Application.Projects.Edit;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.Edit;

public sealed class EditClientRequestValidate
{
    private readonly EditClientValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task Should_Have_Valid_Name()
    {
        EditClientRequest request = new(this.fixture.Create<int>()) { Name = this.fixture.Create<string>() };
        TestValidationResult<EditClientRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_Have_Error_When_ClientId_Invalid(int id)
    {
        EditClientRequest request = new(id) { Name = this.fixture.Create<string>() };
        TestValidationResult<EditClientRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ClientId)
            .WithErrorCode("GreaterThanValidator")
            .WithErrorMessage($"'Client Id' must be greater than '0'.")
            .Only();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Should_Have_Error_When_Name_NotSet(string? name)
    {
        EditClientRequest request = new(this.fixture.Create<int>()) { Name = name! };
        TestValidationResult<EditClientRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage($"'{nameof(EditProjectRequest.Name)}' must not be empty.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Error_When_Name_TooLong()
    {
        EditClientRequest request = new(this.fixture.Create<int>()) { Name = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength) };
        TestValidationResult<EditClientRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(EditProjectRequest.Name)}' must be {ValidationConstants.DefaultTextLength} characters or fewer. You entered {request.Name.Length} characters.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Valid_Note()
    {
        EditClientRequest request = new(this.fixture.Create<int>()) { Note = this.fixture.Create<string>() };
        TestValidationResult<EditClientRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Note);
    }

    [Fact]
    public async Task Should_Have_Error_When_Note_TooLong()
    {
        EditClientRequest request = new(this.fixture.Create<int>())
        {
            Name = this.fixture.Create<string>(),
            Note = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.LongTextLength)
        };

        TestValidationResult<EditClientRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Note)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(EditClientRequest.Note)}' must be {ValidationConstants.LongTextLength} characters or fewer. You entered {request.Note.Length} characters.")
            .Only();
    }
}
