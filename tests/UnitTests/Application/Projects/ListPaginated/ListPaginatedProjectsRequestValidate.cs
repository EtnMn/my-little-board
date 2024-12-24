using AutoFixture.Kernel;
using Etn.MyLittleBoard.Application.Projects.ListPaginated;
using FluentValidation.TestHelper;

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
    public async Task CreateProjectRequestValidate_ShouldBeValidWhenSkipTakeSet()
    {
        ListPaginatedProjectsRequest request = new(this.fixture.Create<int>(), this.fixture.Create<int>());
        TestValidationResult<ListPaginatedProjectsRequest> result = await this.validator.TestValidateAsync(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task CreateProjectRequestValidate_ShouldHaveErrorWhenSkipNegative()
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
    public async Task CreateProjectRequestValidate_ShouldHaveErrorWhenTakeNegative()
    {
        ListPaginatedProjectsRequest request = new(this.fixture.Create<int>(), -1);
        TestValidationResult<ListPaginatedProjectsRequest> result = await this.validator.TestValidateAsync(request);
        result
            .ShouldHaveValidationErrorFor(x => x.Take)
            .WithErrorCode("GreaterThanOrEqualValidator")
            .WithErrorMessage($"'{nameof(ListPaginatedProjectsRequest.Take)}' must be greater than or equal to '0'.")
            .Only();
    }

    public sealed class RandomZeroIntGenerator : ISpecimenBuilder
    {
        private readonly Random random = new();

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(int))
            {
                return this.random.Next(0, 100);
            }
            else
            {
                return new NoSpecimen();
            }
        }
    }
}
