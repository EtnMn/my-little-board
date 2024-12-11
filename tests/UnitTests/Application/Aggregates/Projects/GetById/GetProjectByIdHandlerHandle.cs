using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Application.Projects.GetById;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects.GetById;

public sealed class GetProjectByIdHandlerHandle
{
    private readonly Fixture fixture = new();

    [Fact]
    public async Task GetProjectByIdHandler_GetProject()
    {
        Project project = this.fixture.Build<Project>().With(x => x.Id, ProjectId.From(this.fixture.Create<int>())).Create();
        IRepository<Project> repository = Substitute.For<IRepository<Project>>();
        repository.GetByIdAsync(project.Id.Value, Arg.Any<CancellationToken>()).Returns(project);

        GetProjectByIdHandler handler = new(repository);
        GetProjectByIdRequest request = new(project.Id.Value);
        Result<Project> result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(project);
    }
}
