using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vogen;

namespace Etn.MyLittleBoard.Infrastructure.Data.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasValueGenerator<IdValueGenerator<AppDbContext, Project, ProjectId>>()
            .HasVogenConversion()
            .IsRequired();

        builder.Property(p => p.Name)
            .HasVogenConversion()
            .HasMaxLength(DataSchemaConstants.DefaultNameLength)
            .IsRequired();
    }
}

[EfCoreConverter<ProjectName>]
[EfCoreConverter<ProjectId>]
internal sealed partial class ProjectEfCoreConverters;
