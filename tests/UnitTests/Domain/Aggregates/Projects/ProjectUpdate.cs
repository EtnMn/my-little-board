using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectUpdate
{
    private readonly Fixture fixture = new();
    private readonly Project project;
    private readonly string name;

    public ProjectUpdate()
    {
        this.project = new(
            ProjectName.From(this.fixture.Create<string>()),
            ProjectDescription.Unspecified);

        this.name = this.fixture.Create<string>();
    }

    [Fact]
    public void Project_UpdateName()
    {
        this.project.UpdateName(ProjectName.From(this.name));
        this.project.Name.Value.Should().Be(this.name);
    }
}
