using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectUpdate
{
    private readonly Fixture fixture = new();
    private readonly Project project;

    public ProjectUpdate()
    {
        this.project = new(
            ProjectName.From(this.fixture.Create<string>()),
            ProjectDescription.Unspecified);

    }

    [Fact]
    public void UpdateColor()
    {
        string color = StringHelpers.GenerateHexColor();
        this.project.UpdateColor(ProjectColor.From(color));
        this.project.Color.Value.Should().Be(color);
    }

    [Fact]
    public void UpdateDescription()
    {
        string description = this.fixture.Create<string>();
        this.project.UpdateDescription(ProjectDescription.From(description));
        this.project.Description.Value.Should().Be(description);
    }

    [Fact]
    public void Project_UpdateName()
    {
        string name = this.fixture.Create<string>();
        this.project.UpdateName(ProjectName.From(name));
        this.project.Name.Value.Should().Be(name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Project_UpdatePeriod(int dayOffset)
    {
        DateTime start = this.fixture.Create<DateTime>();
        DateTime end = start.AddDays(dayOffset);
        this.project.UpdatePeriod(ProjectStart.From(start), ProjectEnd.From(end));
        this.project.Start.Value.Should().Be(start);
        this.project.End.Value.Should().Be(end);
    }

    [Fact]
    public void Project_UpdatePeriod_ThrowsException_WhenEndIsSmallerThanStart()
    {
        DateTime start = this.fixture.Create<DateTime>();
        DateTime end = start.AddDays(-1);
        Action action = () => this.project.UpdatePeriod(ProjectStart.From(start), ProjectEnd.From(end));

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Project_UpdateStatus()
    {
        ProjectStatus status = this.fixture.Create<ProjectStatus>();
        this.project.UpdateStatus(status);

        this.project.Status.Should().Be(status);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Project_UpdateStatus_ThrowsException_WhenStatusIsNotDefined(int status)
    {
        Action action = () => this.project.UpdateStatus((ProjectStatus)status);

        action.Should().Throw<ArgumentException>();
    }
}
