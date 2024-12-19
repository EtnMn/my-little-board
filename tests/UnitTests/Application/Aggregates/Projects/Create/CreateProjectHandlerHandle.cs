using Etn.MyLittleBoard.Application.Projects.Create;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Aggregates.Projects.Create;

public sealed class CreateProjectHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Project> repository;
    private readonly int projectId;
    private readonly string projectName;
    private readonly string projectDescription;

    public CreateProjectHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Project>>();
        this.projectId = this.fixture.Create<int>();
        this.projectName = this.fixture.Create<string>();
        this.projectDescription = this.fixture.Create<string>();
        this.repository.AddAsync(Arg.Any<Project>(), Arg.Any<CancellationToken>()).Returns(
            new Project(ProjectName.From(this.projectName), ProjectDescription.From(this.projectDescription))
            {
                Id = ProjectId.From(this.projectId)
            });
    }

    [Fact]
    public async Task CreateProjectHandler_CreateProject()
    {
        IUserService userService = Substitute.For<IUserService>();
        userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());

        CreateProjectHandler handler = new(this.repository, userService);
        CreateProjectRequest request = new() { Name = this.projectName };
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<ProjectId>();
        await this.repository.Received().AddAsync(Arg.Any<Project>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateProjectHandler_ReturnUnauthorizedWhenNoUser()
    {
        IUserService userService = Substitute.For<IUserService>();
        CreateProjectHandler handler = new(this.repository, userService);
        CreateProjectRequest request = new();
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task CreateProjectHandler_ReturnForbiddenWhenNotAdmin()
    {
        IUserService userService = Substitute.For<IUserService>();
        userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, false).Create());

        CreateProjectHandler handler = new(this.repository, userService);
        CreateProjectRequest request = new();
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }
}
