using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectConstructor
{
    private const string projectName = "MyProject";

    [Fact]
    public void Should_Initialize_Project_Name()
    {
        Project project = new(ProjectName.From(projectName));
        Assert.Equal(projectName, project.Name.Value);
    }
}
