using Etn.MyLittleBoard.Application.Projects.Edit;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.Edit;

public sealed class EditProjectRequestValidate
{
    private readonly EditProjectValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task Should_Have_Valid_Name()
    {
        EditProjectRequest request = new(this.fixture.Create<int>()) { Name = this.fixture.Create<string>() };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_Have_Error_When_ProjectId_Invalid(int id)
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
    public async Task Should_Have_Error_When_Name_NotSet(string? name)
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
    public async Task Should_Have_Error_When_Name_TooLong()
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
    public async Task Should_Have_Valid_Description()
    {
        EditProjectRequest request = new(this.fixture.Create<int>()) { Description = this.fixture.Create<string>() };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public async Task Should_Have_Error_When_Description_TooLong()
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
    public async Task Should_Have_Valid_Color()
    {
        string color = StringHelpers.GenerateHexColor();
        EditProjectRequest request = new(this.fixture.Create<int>()) { Color = color };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(x => x.Color);
    }

    [Fact]
    public async Task Should_Have_Error_When_Not_Hex_Color()
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

    [Fact]
    public async Task Should_Have_Error_When_ClientId_Is_Negative()
    {
        EditProjectRequest request = new(this.fixture.Create<int>())
        {
            Name = this.fixture.Create<string>(),
            ClientId = -1,
        };

        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ClientId)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'Client Id' must be greater than or equal to '0'.")
            .Only();
    }

    [Fact]
    public async Task Should_Be_Valid_When_Client_Set()
    {
        EditProjectRequest request = new(this.fixture.Create<int>())
        {
            ClientId = this.fixture.Create<int>(),
            Name = this.fixture.Create<string>(),
        };
        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    public async Task Should_Be_Valid_When_Client_Unset(int? clientId)
    {
        EditProjectRequest request = new(this.fixture.Create<int>())
        {
            Name = this.fixture.Create<string>(),
            ClientId = clientId
        };

        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_End_Is_Smaller_Than_Start()
    {
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now.AddDays(-1);
        EditProjectRequest request = new(this.fixture.Create<int>())
        {
            Name = this.fixture.Create<string>(),
            Start = start,
            End = end,
        };

        TestValidationResult<EditProjectRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.Start)
            .WithErrorCode("LessThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(EditProjectRequest.Start)}' must be less than or equal to '{end}'.");

        result
            .ShouldHaveValidationErrorFor(x => x.End)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(EditProjectRequest.End)}' must be greater than or equal to '{start}'.");
    }
}
