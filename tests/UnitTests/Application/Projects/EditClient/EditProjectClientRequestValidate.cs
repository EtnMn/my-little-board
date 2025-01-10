using Etn.MyLittleBoard.Application.Projects.EditClient;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.EditClient;

public sealed class EditProjectClientRequestValidate
{
    private readonly EditProjectClientValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task Should_Be_Valid_When_Client_Set()
    {
        EditProjectClientRequest request = new(this.fixture.Create<int>(), this.fixture.Create<int>());
        TestValidationResult<EditProjectClientRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    public async Task Should_Be_Valid_When_Client_Unset(int? clientId)
    {
        EditProjectClientRequest request = new(this.fixture.Create<int>(), clientId);
        TestValidationResult<EditProjectClientRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_Have_Error_When_ProjectId_NotSet(int id)
    {
        EditProjectClientRequest request = new(id, this.fixture.Create<int>());
        TestValidationResult<EditProjectClientRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorCode("GreaterThanValidator")
            .WithErrorMessage("'Project Id' must be greater than '0'.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Error_When_ClientId_Is_Negative()
    {
        EditProjectClientRequest request = new(this.fixture.Create<int>(), -1);
        TestValidationResult<EditProjectClientRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ClientId)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage("'Client Id' must be greater than or equal to '0'.")
            .Only();
    }
}
