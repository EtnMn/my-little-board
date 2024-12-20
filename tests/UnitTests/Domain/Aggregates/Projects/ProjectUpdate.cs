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
    public void Should_Update_ProjectColor()
    {
        string color = StringHelpers.GenerateHexColor();
        this.project.UpdateColor(ProjectColor.From(color));
        this.project.Color.Value.Should().Be(color);
    }

    [Fact]
    public void Should_Update_ProjectDescription()
    {
        string description = this.fixture.Create<string>();
        this.project.UpdateDescription(ProjectDescription.From(description));
        this.project.Description.Value.Should().Be(description);
    }

    [Fact]
    public void Should_Update_ProjectName()
    {
        string name = this.fixture.Create<string>();
        this.project.UpdateName(ProjectName.From(name));
        this.project.Name.Value.Should().Be(name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Should_Update_Project_Period(int dayOffset)
    {
        DateTime start = this.fixture.Create<DateTime>();
        DateTime end = start.AddDays(dayOffset);
        this.project.UpdatePeriod(ProjectStart.From(start), ProjectEnd.From(end));
        this.project.Start.Value.Should().Be(start);
        this.project.End.Value.Should().Be(end);
    }

    [Fact]
    public void Should_ThrowException_When_Project_EndIsSmallerThanStart()
    {
        DateTime start = this.fixture.Create<DateTime>();
        DateTime end = start.AddDays(-1);
        Action action = () => this.project.UpdatePeriod(ProjectStart.From(start), ProjectEnd.From(end));
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Should_Update_ProjectStatus()
    {
        ProjectStatus status = this.fixture.Create<ProjectStatus>();
        this.project.UpdateStatus(status);
        this.project.Status.Should().Be(status);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_ThrowException_When_ProjectStatus_IsNotDefined(int status)
    {
        Action action = () => this.project.UpdateStatus((ProjectStatus)status);
        action.Should().Throw<ArgumentException>();
    }
}
