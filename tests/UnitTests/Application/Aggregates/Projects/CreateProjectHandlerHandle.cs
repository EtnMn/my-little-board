using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Application.Projects.Create;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects;

public sealed class CreateProjectHandlerHandle
{
    private readonly Fixture fixture = new();

    [Fact]
    public async Task CreateProjectHandler_CreateProject()
    {
        IRepository<Project> repository = Substitute.For<IRepository<Project>>();
        string name = this.fixture.Create<string>();
        repository.AddAsync(Arg.Any<Project>(), Arg.Any<CancellationToken>()).Returns(new Project(ProjectName.From(name)));

        CreateProjectHandler handler = new(repository);
        CreateProjectRequest request = new(name);
        Ardalis.Result.Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<ProjectId>();
    }
}
