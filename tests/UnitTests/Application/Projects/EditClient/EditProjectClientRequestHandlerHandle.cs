using Etn.MyLittleBoard.Application.Projects.EditClient;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.EditClient;

public sealed class EditProjectClientRequestHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Project> repository;
    private readonly IUserService userService;

    public EditProjectClientRequestHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Project>>();
        this.userService = Substitute.For<IUserService>();
    }

    [Fact]
    public async Task Should_Set_Client()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());

        Project project = this.fixture
            .Build<Project>()
            .With(x => x.Id, ProjectId.From(this.fixture.Create<int>()))
            .Create();

        this.repository
            .GetByIdAsync(project.Id, Arg.Any<CancellationToken>())
            .Returns(project);

        int clientId = this.fixture.Create<int>();
        EditProjectClientRequest request = new(project.Id.Value, clientId);
        EditProjectClientHandler handler = new(this.repository, this.userService);

        Result result = await handler.Handle(request, CancellationToken.None);
        _ = this.userService.Received().AuthenticatedUser;

        await this.repository.Received().GetByIdAsync(Arg.Any<ProjectId>(), Arg.Any<CancellationToken>());
        await this.repository.Received().UpdateAsync(project, Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
        project.ClientId.Should().Be(ProjectClientId.From(clientId));
    }

    // Unset
    // Not found
    // Unautho
    // Forbiden
}
