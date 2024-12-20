using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectNameFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void ProjectName_CreatesProjectName()
    {
        string name = this.fixture.Create<string>();
        ProjectName projectName = ProjectName.From(name);
        projectName.Should().NotBeNull();
        projectName.Value.Should().Be(name);
    }

    [Theory]
    [MemberData(nameof(StringValidationExceptionData))]
    public void ProjectName_ThrowsException_WhenNameIsNotSet(string? value)
    {
        Action action = () => ProjectName.From(value!);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Theory]
    [InlineData(" x")]
    [InlineData("x ")]
    [InlineData(" x ")]
    public void Should_Trim_ProjectName_Value(string value)
    {
        ProjectName projectName = ProjectName.From(value);
        projectName.Value.Should().Be(value.Trim());
    }
    public static TheoryData<string?> StringValidationExceptionData => new()
    {
        { null },
        { string.Empty },
        { " " },
        { StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength) }
    };
}
