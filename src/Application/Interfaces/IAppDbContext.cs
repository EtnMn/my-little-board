using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;

namespace Etn.MyLittleBoard.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<Project> Projects { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}
