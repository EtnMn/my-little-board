using Etn.MyLittleBoard.Application.Projects.ListPaginated;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.ListPaginated;

public sealed class ListPaginatedProjectsRequestValidate
{
    private readonly ListPaginatedProjectsValidator validator = new();
    private readonly Fixture fixture = new();

    public ListPaginatedProjectsRequestValidate()
    {
        this.fixture.Customizations.Add(new RandomZeroIntGenerator());
    }

    [Fact]
    public async Task Should_Be_Valid_When_Skip_Take_Set()
    {
        ListPaginatedProjectsRequest request = new(this.fixture.Create<int>(), this.fixture.Create<int>());
        TestValidationResult<ListPaginatedProjectsRequest> result = await this.validator.TestValidateAsync(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Skip_Negative()
    {
        ListPaginatedProjectsRequest request = new(-1, this.fixture.Create<int>());
        TestValidationResult<ListPaginatedProjectsRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Skip)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(ListPaginatedProjectsRequest.Skip)}' must be greater than or equal to '0'.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Error_When_Take_Negative()
    {
        ListPaginatedProjectsRequest request = new(this.fixture.Create<int>(), -1);
        TestValidationResult<ListPaginatedProjectsRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Take)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(ListPaginatedProjectsRequest.Take)}' must be greater than or equal to '0'.")
            .Only();
    }
}
