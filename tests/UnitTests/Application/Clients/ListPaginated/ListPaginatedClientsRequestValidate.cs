using Etn.MyLittleBoard.Application.Clients.ListPaginated;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.ListPaginated;

public sealed class ListPaginatedClientsRequestValidate
{
    private readonly ListPaginatedClientsValidator validator = new();
    private readonly Fixture fixture = new();

    public ListPaginatedClientsRequestValidate()
    {
        this.fixture.Customizations.Add(new RandomZeroIntGenerator());
    }

    [Fact]
    public async Task Should_Be_Valid_When_Skip_Take_Set()
    {
        ListPaginatedClientsRequest request = new(
            this.fixture.Create<string>(),
            this.fixture.Create<int>(),
            this.fixture.Create<int>(),
            this.fixture.Create<bool>());

        TestValidationResult<ListPaginatedClientsRequest> result = await this.validator.TestValidateAsync(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_Have_Error_When_Skip_Negative()
    {
        ListPaginatedClientsRequest request = new(
            this.fixture.Create<string>(),
            -1,
            this.fixture.Create<int>(),
            this.fixture.Create<bool>());

        TestValidationResult<ListPaginatedClientsRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Skip)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(ListPaginatedClientsRequest.Skip)}' must be greater than or equal to '0'.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Error_When_Take_Negative()
    {
        ListPaginatedClientsRequest request = new(
            this.fixture.Create<string>(),
            this.fixture.Create<int>(),
            -1,
            this.fixture.Create<bool>());

        TestValidationResult<ListPaginatedClientsRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Take)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(ListPaginatedClientsRequest.Take)}' must be greater than or equal to '0'.")
            .Only();
    }

    [Fact]
    public async Task Should_Have_Error_When_Search_Too_Long()
    {
        ListPaginatedClientsRequest request = new(
            StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength),
            this.fixture.Create<int>(),
            this.fixture.Create<int>(),
            this.fixture.Create<bool>());

        TestValidationResult<ListPaginatedClientsRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Search)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"The length of '{nameof(ListPaginatedClientsRequest.Search)}' must be {ValidationConstants.DefaultTextLength} characters or fewer. You entered {request.Search.Length} characters.")
            .Only();
    }
}
