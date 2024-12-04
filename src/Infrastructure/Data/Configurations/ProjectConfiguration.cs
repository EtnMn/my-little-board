using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .HasMaxLength(ValidationConstants.DefaultNameLength)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasVogenConversion()
            .HasMaxLength(ValidationConstants.DefaultNameLength)
            .IsRequired();

        builder.Property(p => p.Start)
            .HasVogenConversion()
            .IsRequired();

        builder.Property(p => p.End)
            .HasVogenConversion()
            .IsRequired();
    }
}

[EfCoreConverter<ProjectDescription>]
[EfCoreConverter<ProjectEnd>]
[EfCoreConverter<ProjectId>]
[EfCoreConverter<ProjectName>]
[EfCoreConverter<ProjectStart>]
internal sealed partial class ProjectEfCoreConverters;
