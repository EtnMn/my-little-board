using Etn.MyLittleBoard.Application.Clients.ListPaginated;
using Etn.MyLittleBoard.Application.Shared;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.ListPaginated;

public sealed class ListPaginatedClientsHandlerHandle
{
    private readonly Fixture fixture = new();

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    public async Task Should_Get_Right_Clients_Count(int count)
    {
        IEnumerable<Client> clients = this.fixture.CreateMany<Client>(count);
        IRepository<Client> repository = Substitute.For<IRepository<Client>>();
        repository.CountAsync(Arg.Any<ClientsPaginated>(), Arg.Any<CancellationToken>()).Returns(count);
        repository.ListAsync(Arg.Any<ClientsPaginated>(), Arg.Any<CancellationToken>()).Returns([.. clients]);

        ListPaginatedClientsHandler handler = new(repository);
        ListPaginatedClientsRequest request = new(string.Empty, 0, count, true);
        Result<PageDto<Client>> result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Skip.Should().Be(request.Skip);
        result.Value.Take.Should().Be(request.Take);
        result.Value.Count.Should().Be(count);
        result.Value.Items.Should().NotBeNull();
        result.Value.Items.Should().BeOfType<Client[]>();
        result.Value.Items.Should().HaveCount(count);
        await repository.Received().CountAsync(Arg.Any<ClientsPaginated>(), Arg.Any<CancellationToken>());
        await repository.Received().ListAsync(Arg.Any<ClientsPaginated>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("", 3)]
    [InlineData("x", 2)]
    [InlineData("xx", 1)]
    [InlineData("XX", 1)]
    [InlineData(" xx ", 1)]
    [InlineData("z", 0)]
    public async Task Should_Filter_By_Name(string search, int count)
    {
        Queue<string> names = new(["x", "xx", "y"]);
        IEnumerable<Client> clients = this.fixture
            .Build<Client>()
            .FromFactory(() => new Client(ClientName.From(names.Dequeue()), ClientNote.Unspecified))
            .CreateMany(names.Count);

        IRepository<Client> repository = Substitute.For<IRepository<Client>>();
        repository.CountAsync(Arg.Any<ClientsPaginated>(), Arg.Any<CancellationToken>()).Returns(count);
        repository.ListAsync(Arg.Any<ClientsPaginated>(), Arg.Any<CancellationToken>()).Returns([.. clients]);

        ListPaginatedClientsHandler handler = new(repository);
        ListPaginatedClientsRequest request = new(search, 0, names.Count, true);
        Result<PageDto<Client>> result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Count.Should().Be(count);
    }
}
