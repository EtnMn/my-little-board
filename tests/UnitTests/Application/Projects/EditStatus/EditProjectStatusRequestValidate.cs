using Etn.MyLittleBoard.Application.Projects.EditStatus;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using FluentValidation.TestHelper;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.EditStatus;

public sealed class EditProjectStatusRequestValidate
{
    private readonly EditProjectStatusValidator validator = new();
    private readonly Fixture fixture = new();

    [Fact]
    public async Task EditProjectStatusRequestValidate_ShouldBeValidWhenStatusSet()
    {
        EditProjectStatusRequest request = new(this.fixture.Create<int>(), this.fixture.Create<ProjectStatus>());
        TestValidationResult<EditProjectStatusRequest> result = await this.validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task EditProjectStatusRequestValidate_ShouldHaveErrorWhenProjectIdNotSet(int id)
    {
        EditProjectStatusRequest request = new(id, this.fixture.Create<ProjectStatus>());
        TestValidationResult<EditProjectStatusRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ProjectId)
            .WithErrorCode("GreaterThanValidator")
            .WithErrorMessage("'Project Id' must be greater than '0'.")
            .Only();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public async Task EditProjectStatusRequestValidate_ShouldHaveErrorWhenStatusNotSet(int status)
    {
        EditProjectStatusRequest request = new(this.fixture.Create<int>(), (ProjectStatus)status);
        TestValidationResult<EditProjectStatusRequest> result = await this.validator.TestValidateAsync(request);

        result
            .ShouldHaveValidationErrorFor(x => x.ProjectStatus)
            .WithErrorCode("EnumValidator")
            .WithErrorMessage($"'Project Status' has a range of values which does not include '{status}'.")
            .Only();
    }
}
