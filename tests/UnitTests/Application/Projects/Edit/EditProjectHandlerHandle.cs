using Etn.MyLittleBoard.Application.Projects.Edit;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Application.Projects.Edit;

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
    public async Task Should_Update_Project_Values()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

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
        EditProjectRequest request = new(project.Id.Value)
        {
            Name = this.fixture.Create<string>(),
            Description = this.fixture.Create<string>(),
            Color = StringHelpers.GenerateHexColor()
        };

        Result result = await handler.Handle(request, CancellationToken.None);

        _ = this.userService.Received().AuthenticatedUser;
        await this.repository.Received().GetByIdAsync(Arg.Any<ProjectId>(), Arg.Any<CancellationToken>());
        await this.repository.Received().UpdateAsync(project, Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Project_Does_Not_Exist()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        EditProjectHandler handler = new(this.repository, this.userService);
        EditProjectRequest request = new(this.fixture.Create<int>());
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task Should_Return_Unauthorized_When_User_Not_Authenticated()
    {
        EditProjectHandler handler = new(this.repository, this.userService);
        EditProjectRequest request = new(this.fixture.Create<int>());
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_User_Not_Administrator()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, false)
            .Create());

        EditProjectHandler handler = new(this.repository, this.userService);
        EditProjectRequest request = new(this.fixture.Create<int>());
        Result<ProjectId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }
}
