using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectColorFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_ProjectColor()
    {
        string color = StringHelpers.GenerateHexColor();
        ProjectColor projectName = ProjectColor.From(color);
        projectName.Should().NotBeNull();
        projectName.Value.Should().Be(color);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Be_Unspecified_When_ProjectColor_IsNullOrEmpty(string? value)
    {
        ProjectColor projectName = ProjectColor.From(value!);
        projectName.Should().NotBeNull();
        projectName.Value.Should().Be(ProjectColor.Unspecified.Value);
    }

    [Fact]
    public void Should_ThrowException_When_ProjectColor_IsNotHexColor()
    {
        string color = this.fixture.Create<string>();
        Action action = () => ProjectColor.From(color);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Fact]
    public void Should_Trim_ProjectColor_Value()
    {
        string color = $" {StringHelpers.GenerateHexColor()} ";
        ProjectColor projectColor = ProjectColor.From(color);
        projectColor.Value.Should().Be(color.Trim());
    }
}
