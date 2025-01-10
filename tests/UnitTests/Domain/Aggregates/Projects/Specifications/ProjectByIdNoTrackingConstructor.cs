using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectByIdNoTrackingConstructor
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Filter_Project_By_Id()
    {
        int i = 0;
        IEnumerable<Project> projects = this.fixture
          .Build<Project>()
          .FromFactory(() => new Project(ProjectName.From(this.fixture.Create<string>()), ProjectDescription.Unspecified))
          .With(p => p.Id, () => ProjectId.From(++i))
          .CreateMany(this.fixture.Create<int>());

        ProjectByIdNoTracking specification = new(projects.First().Id);
        IEnumerable<Project> result = specification.Evaluate(projects);

        result.Should().ContainSingle();
        result.First().Id.Should().Be(projects.First().Id);
    }
}
