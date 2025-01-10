using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects;

public sealed class ProjectClientIdFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_ProjectClientId()
    {
        int clientId = this.fixture.Create<int>();
        ProjectClientId projectClientId = ProjectClientId.From(clientId);
        projectClientId.Should().NotBeNull();
        projectClientId.Value.Should().Be(clientId);
    }

    [Fact]
    public void Should_Initialize_When_ProjectClientId_Is_Zero()
    {
        const int clientId = 0;
        ProjectClientId projectClientId = ProjectClientId.From(clientId);
        projectClientId.Should().NotBeNull();
        projectClientId.Value.Should().Be(clientId);
    }

    [Fact]
    public void Should_ThrowException_When_ProjectClientId_Is_Negative()
    {
        Action action = () => ProjectClientId.From(-1);
        action.Should().Throw<ValueObjectValidationException>();
    }
}
