using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Application.Projects.ListPaginated;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects.ListPaginated;

public sealed class ListPaginatedProjectsHandlerHandle
{
    private readonly Fixture fixture = new();

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    public async Task ListPaginatedProjectsHandler_ListProjects(int count)
    {
        IEnumerable<Project> projects = this.fixture.CreateMany<Project>(count);
        IRepository<Project> repository = Substitute.For<IRepository<Project>>();
        repository.GetAllAsync(Arg.Any<ProjectsPaginated>(), Arg.Any<CancellationToken>()).Returns([.. projects]);

        ListPaginatedProjectsHandler handler = new(repository);
        ListPaginatedProjectsRequest request = new(0, count);
        Result<Project[]> result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<Project[]>();
        result.Value.Should().HaveCount(count);
    }
}
