using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectsPaginatedConstructor
{
    private readonly Fixture fixture = new();

    public ProjectsPaginatedConstructor()
    {
        this.fixture.Register(() => new Project(ProjectName.From(this.fixture.Create<string>())));
    }

    [Theory]
    [InlineData(100, 0, 10)]
    [InlineData(100, 5, 5)]
    [InlineData(0, 0, 5)]
    [InlineData(0, 0, 0)]
    [InlineData(10, 10, 10)]
    public void ProjectsPaginatedConstructor_ReturnList(int count, int skip, int take)
    {
        ProjectsPaginated specifications = new(skip, take);
        IEnumerable<Project> projects = this.fixture.CreateMany<Project>(count);
        IEnumerable<Project> result = specifications.Evaluate(projects);

        result.Should().HaveCount(Math.Min(take, Math.Max(count - skip, 0)));
    }

    [Fact]
    public void ProjectsPaginatedConstructor_ThrowsException_WhenSkipIsNegative()
    {
        Action action = () => new ProjectsPaginated(-1, this.fixture.Create<int>());
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ProjectsPaginatedConstructor_ThrowsException_WhenTakeIsNegative()
    {
        Action action = () => new ProjectsPaginated(this.fixture.Create<int>(), -1);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}
