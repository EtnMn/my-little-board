using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients.Specifications;

public sealed class ClientsPaginatedConstructor
{
    private readonly Fixture fixture = new();

    public ClientsPaginatedConstructor()
    {
        this.fixture.Register(() => new Client(ClientName.From(this.fixture.Create<string>()), ClientNote.Unspecified));
    }

    [Theory]
    [InlineData(100, 0, 10)]
    [InlineData(100, 5, 5)]
    [InlineData(0, 0, 5)]
    [InlineData(0, 0, 0)]
    [InlineData(10, 10, 10)]
    public void Should_Return_Right_Clients_Count(int count, int skip, int take)
    {
        ClientsPaginated specifications = new(string.Empty, skip, take, this.fixture.Create<bool>(), this.fixture.Create<bool>());
        IEnumerable<Client> clients = this.fixture.CreateMany<Client>(count);
        IEnumerable<Client> result = specifications.Evaluate(clients);

        result.Should().HaveCount(Math.Min(take, Math.Max(count - skip, 0)));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Should_Sort_By_Name(bool descending)
    {
        Queue<string> names = new(["a", "b", "c"]);
        IEnumerable<Client> clients = this.fixture
            .Build<Client>()
            .FromFactory(() => new Client(ClientName.From(names.Dequeue()), ClientNote.Unspecified))
            .CreateMany(names.Count);

        ClientsPaginated specifications = new(string.Empty, 0, names.Count, descending, this.fixture.Create<bool>());
        IEnumerable<Client> result = specifications.Evaluate(clients);

        if (descending)
        {
            result.Select(x => x.Name).Should().BeInDescendingOrder();
        }
        else
        {
            result.Select(x => x.Name).Should().BeInAscendingOrder();
        }
    }

    [Fact]
    public void Should_ThrowException_When_Skip_IsNegative()
    {
        Action action = () => _ = new ClientsPaginated(
            string.Empty,
            -1,
            this.fixture.Create<int>(),
            this.fixture.Create<bool>(),
            this.fixture.Create<bool>());

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_ThrowException_When_Take_IsNegative()
    {
        Action action = () => _ = new ClientsPaginated(
            string.Empty,
            this.fixture.Create<int>(),
            -1,
            this.fixture.Create<bool>(),
            this.fixture.Create<bool>());

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_ThrowException_When_Search_IsNull()
    {
        Action action = () => _ = new ClientsPaginated(
            null!,
            this.fixture.Create<int>(),
            this.fixture.Create<int>(),
            this.fixture.Create<bool>(),
            this.fixture.Create<bool>());

        action.Should().Throw<ArgumentNullException>().WithParameterName("search");
    }

    [Fact]
    public void Should_Exclude_Disabled_Clients()
    {
        const int count = 10;
        ClientsPaginated specifications = new(string.Empty, 0, count, this.fixture.Create<bool>(), this.fixture.Create<bool>());
        IEnumerable<Client> clients = this.fixture.CreateMany<Client>(count);
        IEnumerable<Client> result = specifications.Evaluate(clients);

        result.Should().HaveCount(clients.Count(c => c.State == ClientState.Enabled));
    }

    [Fact]
    public void Should_Include_All_Clients()
    {
        const int count = 10;
        ClientsPaginated specifications = new(string.Empty, 0, count, this.fixture.Create<bool>(), this.fixture.Create<bool>());
        IEnumerable<Client> clients = this.fixture.CreateMany<Client>(count);
        IEnumerable<Client> result = specifications.Evaluate(clients);

        result.Should().HaveCount(clients.Count());
    }
}
