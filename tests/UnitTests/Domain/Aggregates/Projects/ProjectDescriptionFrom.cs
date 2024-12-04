using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectDescriptionFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void ProjectDescriptionFrom_CreatesProjectDescription()
    {
        string description = this.fixture.Create<string>();
        ProjectDescription projectDescription = ProjectDescription.From(description);
        projectDescription.Should().NotBeNull();
        projectDescription.Value.Should().Be(description);
    }

    [Theory]
    [MemberData(nameof(StringValidationExceptionData))]
    public void ProjectDescriptionFrom_ThrowsException_WhenDescriptionIsNotValid(string? description)
    {
        Action action = () => ProjectDescription.From(description!);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("    ")]
    public void ProjectDescriptionFrom_MustNormalizeNullAndEmptyValue(string? description)
    {
        ProjectDescription projectDescription = ProjectDescription.From(description!);
        projectDescription.Should().NotBeNull();
        projectDescription.Value.Should().Be(string.Empty);
    }

    public static TheoryData<string?> StringValidationExceptionData => new()
    {
        { new string('x', ValidationConstants.DefaultNameLength + 1) }
    };
}