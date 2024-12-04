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
        Assert.Equal(name, project.Name.Value);
        Assert.Equal(string.Empty, project.Description.Value);
        Assert.Equal(DateTimeOffset.MinValue, project.Start.Value);
        Assert.Equal(DateTimeOffset.MaxValue, project.End.Value);
    }

    [Fact]
    public void Should_Initialize_Project_AllProperties()
    {
        string name = this.fixture.Create<string>();
        string description = this.fixture.Create<string>();
        Project project = new(ProjectName.From(name), ProjectDescription.From(description));
        Assert.Equal(name, project.Name.Value);
        Assert.Equal(description, project.Description.Value);
        Assert.Equal(DateTimeOffset.MinValue, project.Start.Value);
        Assert.Equal(DateTimeOffset.MaxValue, project.End.Value);
    }
}
