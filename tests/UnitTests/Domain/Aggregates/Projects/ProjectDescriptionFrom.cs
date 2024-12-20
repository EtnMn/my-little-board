using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectDescriptionFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_ProjectDescription()
    {
        string description = this.fixture.Create<string>();
        ProjectDescription projectDescription = ProjectDescription.From(description);
        projectDescription.Should().NotBeNull();
        projectDescription.Value.Should().Be(description);
    }

    [Fact]
    public void Should_ThrowException_When_ProjectDescription_ExceedsMaxLength()
    {
        string value = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.LongTextLength);
        Action action = () => ProjectDescription.From(value!);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Should_Be_Unspecified_When_ProjectDescription_IsNullOrEmpty(string? value)
    {
        ProjectDescription projectDescription = ProjectDescription.From(value!);
        projectDescription.Should().NotBeNull();
        Assert.Equal(ProjectDescription.Unspecified, projectDescription);
    }

    [Theory]
    [InlineData(" x")]
    [InlineData("x ")]
    [InlineData(" x ")]
    public void Should_Trim_ProjectDescription_Value(string value)
    {
        ProjectDescription clientNote = ProjectDescription.From(value);
        clientNote.Value.Should().Be(value.Trim());
    }
}
