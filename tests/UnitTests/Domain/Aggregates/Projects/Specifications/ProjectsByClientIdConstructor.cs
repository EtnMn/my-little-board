using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects.Specifications;
public sealed class ProjectsByClientIdConstructor
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Filter_Project_By_ClientId()
    {
        const int key = 1;
        int i = 0;
        Queue<ProjectClientId> clientIds = new([ProjectClientId.From(key), ProjectClientId.From(key + 1), ProjectClientId.Unspecified]);
        IEnumerable<Project> projects = this.fixture
          .Build<Project>()
          .FromFactory(() =>
          {
              Project project = new(ProjectName.From(this.fixture.Create<string>()), ProjectDescription.Unspecified);
              project.SetClient(clientIds.Dequeue());
              return project;
          })
          .With(p => p.Id, () => ProjectId.From(++i))
          .CreateMany(clientIds.Count);

        ProjectsByClientId specification = new(ProjectClientId.From(key));
        IEnumerable<Project> result = specification.Evaluate(projects);

        result.Should().ContainSingle();
        result.First().ClientId.Should().Be(ProjectClientId.From(key));
    }
}
