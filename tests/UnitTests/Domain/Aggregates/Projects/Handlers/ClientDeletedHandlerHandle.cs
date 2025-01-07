using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Events;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Handlers;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;
using Microsoft.Extensions.Logging;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Projects.Handlers;

public sealed class ClientDeletedHandlerHandle
{
    private readonly Fixture fixture = new();

    [Fact]
    public async Task Should_Remove_Client_From_Projects()
    {
        const int key = 1;
        int i = 0;
        Queue<ProjectClientId> clientIds = new([ProjectClientId.From(key), ProjectClientId.From(key + 1), ProjectClientId.Unspecified]);
        IEnumerable<Project> projects = this.fixture
                 .Build<Project>()
                 .FromFactory(() =>
                 {
                     Project project = new(ProjectName.From(this.fixture.Create<string>()), ProjectDescription.Unspecified);
                     project.SetClient(clientIds.Dequeue());
                     return project;
                 })
                 .With(p => p.Id, () => ProjectId.From(++i))
                 .CreateMany(clientIds.Count);

        IRepository<Project> repository = Substitute.For<IRepository<Project>>();
        repository
            .ListAsync(Arg.Any<ProjectsByClientId>(), Arg.Any<CancellationToken>())
            .Returns([.. projects.Where(p => p.ClientId == ProjectClientId.From(key))]);

        ILogger<ClientDeletedHandler> logger = Substitute.For<ILogger<ClientDeletedHandler>>();
        ClientDeletedEvent request = new(ClientId.From(1));
        ClientDeletedHandler handler = new(repository, logger);
        await handler.Handle(request, CancellationToken.None);
        foreach (Project p in projects)
        {
            if (p.Id == ProjectId.From(key))
            {
                await repository.Received().UpdateAsync(p, Arg.Any<CancellationToken>());
            }
            else
            {
                await repository.DidNotReceive().UpdateAsync(p, Arg.Any<CancellationToken>());
            }
        }
    }
}
