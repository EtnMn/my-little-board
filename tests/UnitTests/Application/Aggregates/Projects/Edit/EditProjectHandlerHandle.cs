using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Application.Projects.Edit;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects.Edit;

public sealed class EditProjectHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Project> repository;
    private readonly IUserService userService;

    public EditProjectHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Project>>();
        this.userService = Substitute.For<IUserService>();
    }

    [Fact]
    public async Task EditProjectHandler_EditProjectValues()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());
        Project project = this.fixture
            .Build<Project>()
            .FromFactory<int>((x) => new Project(
                ProjectName.From(this.fixture.Create<string>()),
                ProjectDescription.Unspecified))
            .With(x => x.Id, ProjectId.From(this.fixture.Create<int>()))
            .Create();

        this.repository
            .GetByIdAsync(project.Id, Arg.Any<CancellationToken>())
            .Returns(project);

        EditProjectHandler handler = new(this.repository, this.userService);
        string name = this.fixture.Create<string>();
        string description = this.fixture.Create<string>();
        string color = StringHelpers.GenerateHexColor();
        EditProjectRequest request = new(project.Id.Value) { Name = name };
        Result result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
        await this.repository.Received().GetByIdAsync(Arg.Any<ProjectId>(), Arg.Any<CancellationToken>());

        project.Name.Value.Should().Be(name);
        project.Description.Value.Should().Be(description);
        project.Color.Value.Should().Be(color);
        await this.repository.Received().UpdateAsync(project, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task EditProjectHandler_ReturnNotFoundWhenProjectDoesNotExist()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());

        EditProjectHandler handler = new(this.repository, this.userService);
        EditProjectRequest request = new(this.fixture.Create<int>());
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task EditProjectHandler_ReturnUnauthorizedWhenNoUser()
    {
        EditProjectHandler handler = new(this.repository, this.userService);
        EditProjectRequest request = new(this.fixture.Create<int>());
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task EditProjectHandler_ReturnForbiddenWhenNotAdmin()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, false).Create());

        EditProjectHandler handler = new(this.repository, this.userService);
        EditProjectRequest request = new(this.fixture.Create<int>());
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }
}