using Etn.MyLittleBoard.Application.Clients.Create;


namespace Etn.MyLittleBoard.UnitTests.Application.Clients.Create;

public sealed class CreateClientRequestValidate
{
    private readonly CreateClientValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task Should_Be_Valid_When_NameSet()
    {
        CreateClientRequest request = new() { Name = this.fixture.Create<string>() };
        TestValidationResult<CreateClientRequest> result = await this.validator.TestValidateAsync(request);
        result.ShouldNotHaveAnyValidationErrors();
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Should_Have_Validation_Error_When_NameNotSet(string? name)
    {
        CreateClientRequest request = new() { Name = name! };
        TestValidationResult<CreateClientRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage($"'{nameof(CreateClientRequest.Name)}' must not be empty.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Validation_Error_When_NameTooLong()
    {
        CreateClientRequest request = new()
        {
            Name = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength)
        };

        TestValidationResult<CreateClientRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(CreateClientRequest.Name)}' must be {ValidationConstants.DefaultTextLength} characters or fewer. You entered {request.Name.Length} characters.")
            .Only();
    }
}
