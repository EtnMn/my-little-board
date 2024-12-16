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

    public static TheoryData<string?> StringValidationExceptionData => new()
    {
        { null },
        { string.Empty },
        { " " },
        { StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength) }
    };
}
