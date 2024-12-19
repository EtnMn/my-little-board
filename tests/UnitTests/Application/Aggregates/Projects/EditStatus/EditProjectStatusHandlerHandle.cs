using Etn.MyLittleBoard.Application.Projects.EditStatus;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects.EditStatus;

public sealed class EditProjectStatusHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Project> repository;
    private readonly IUserService userService;

    public EditProjectStatusHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Project>>();
        this.userService = Substitute.For<IUserService>();
    }

    [Fact]
    public async Task EditProjectStatusHandler_EditProjectStatus()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());

        Project project = this.fixture
            .Build<Project>()
            .FromFactory<int>((x) => new Project(
                ProjectName.From(this.fixture.Create<string>()),
                ProjectDescription.Unspecified))
            .With(x => x.Id, ProjectId.From(this.fixture.Create<int>()))
            .Create();

        EditProjectStatusRequest request = new(project.Id.Value, this.fixture.Create<ProjectStatus>());
        EditProjectStatusHandler handler = new(this.repository, this.userService);

        this.repository
            .GetByIdAsync(project.Id, Arg.Any<CancellationToken>())
            .Returns(project);

        Result result = await handler.Handle(request, CancellationToken.None);
        _ = this.userService.Received().AuthenticatedUser;

        await this.repository.Received().GetByIdAsync(Arg.Any<ProjectId>(), Arg.Any<CancellationToken>());
        await this.repository.Received().UpdateAsync(project, Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task EditProjectStatusHandler_ReturnNotFoundWhenProjectDoesNotExist()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());

        EditProjectStatusRequest request = new(this.fixture.Create<int>(), this.fixture.Create<ProjectStatus>());
        EditProjectStatusHandler handler = new(this.repository, this.userService);
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task EditProjectStatusHandler_ReturnUnauthorizedWhenNoUser()
    {
        EditProjectStatusRequest request = new(this.fixture.Create<int>(), this.fixture.Create<ProjectStatus>());
        EditProjectStatusHandler handler = new(this.repository, this.userService);
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task EditProjectStatusHandler_ReturnForbiddenWhenNotAdministrator()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, false).Create());

        EditProjectStatusRequest request = new(this.fixture.Create<int>(), this.fixture.Create<ProjectStatus>());
        EditProjectStatusHandler handler = new(this.repository, this.userService);
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }
}
