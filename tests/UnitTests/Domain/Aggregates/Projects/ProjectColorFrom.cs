using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectColorFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void ProjectColorFrom_CreatesProjectColorFrom()
    {
        string color = StringHelpers.GenerateHexColor();
        ProjectColor projectName = ProjectColor.From(color);
        projectName.Should().NotBeNull();
        projectName.Value.Should().Be(color);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ProjectColorFrom_CreatesUnspecifiedProjectColorFrom(string value)
    {
        ProjectColor projectName = ProjectColor.From(value);
        projectName.Should().NotBeNull();
        projectName.Value.Should().Be(ProjectColor.Unspecified.Value);
    }

    [Fact]
    public void ProjectColorFrom_ThrowsException_WhenNotHexColor()
    {
        string color = this.fixture.Create<string>();
        Action action = () => ProjectColor.From(color);
        action.Should().Throw<ValueObjectValidationException>();
    }

    public static TheoryData<string?> StringValidationExceptionData => new()
    {
        { string.Empty },
        { " " },
    };
}
