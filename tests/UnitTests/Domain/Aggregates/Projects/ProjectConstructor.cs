using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectConstructor
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_Project_MandatoryProperties()
    {
        string name = this.fixture.Create<string>();
        Project project = new(ProjectName.From(name), ProjectDescription.Unspecified);
        project.Name.Value.Should().Be(name);
        Assert.Equal(ProjectDescription.Unspecified, project.Description);
        Assert.Equal(ProjectColor.Unspecified, project.Color);
        Assert.Equal(ProjectStart.Unspecified, project.Start);
        Assert.Equal(ProjectEnd.Unspecified, project.End);
        Assert.Equal(ProjectStatus.Draft, project.Status);
    }

    [Fact]
    public void Should_Initialize_Project_Properties()
    {
        string name = this.fixture.Create<string>();
        string description = this.fixture.Create<string>();
        Project project = new(ProjectName.From(name), ProjectDescription.From(description));
        project.Name.Value.Should().Be(name);
        project.Description.Value.Should().Be(description);
        Assert.Equal(ProjectColor.Unspecified, project.Color);
        Assert.Equal(ProjectStart.Unspecified, project.Start);
        Assert.Equal(ProjectEnd.Unspecified, project.End);
        Assert.Equal(ProjectStatus.Draft, project.Status);
    }
}
