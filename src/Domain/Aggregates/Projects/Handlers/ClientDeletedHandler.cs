using Etn.MyLittleBoard.Domain.Aggregates.Clients.Events;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;
using Etn.MyLittleBoard.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects.Handlers;

public sealed class ClientDeletedHandler(
    IRepository<Project> repository,
    ILogger<ClientDeletedHandler> logger) :
    INotificationHandler<ClientDeletedEvent>
{
    public async Task Handle(ClientDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Removing deleted client {ClientId} from projects",
            notification.ClientId);

        ProjectsByClientId specification = new(ProjectClientId.From(notification.ClientId));
        List<Project> projects = await repository.ListAsync(specification, cancellationToken);
        foreach (Project p in projects)
        {
            p.RemoveClient();
            await repository.UpdateAsync(p, cancellationToken);
        }
    }
}
