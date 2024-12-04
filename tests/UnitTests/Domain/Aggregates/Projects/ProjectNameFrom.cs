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
    public void ProjectName_ThrowsException_WhenNameIsNotSet(string? name)
    {
        Action action = () => ProjectName.From(name!);
        action.Should().Throw<ValueObjectValidationException>();
    }

    public static TheoryData<string?> StringValidationExceptionData => new()
    {
        { null },
        { string.Empty },
        { " " },
        { new string('x', ValidationConstants.DefaultNameLength + 1) }
    };
}
