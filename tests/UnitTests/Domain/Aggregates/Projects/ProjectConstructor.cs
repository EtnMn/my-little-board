using AutoFixture;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectConstructor
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_Project_Name()
    {
        string name = this.fixture.Create<string>();
        Project project = new(ProjectName.From(name));
        Assert.Equal(name, project.Name.Value);
    }
}
