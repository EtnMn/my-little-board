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
            .HasMaxLength(ValidationConstants.DefaultTextLength)
            .IsRequired();

        builder.Property(p => p.Color)
            .HasVogenConversion()
            .HasMaxLength(ValidationConstants.ShortTextLength)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasVogenConversion()
            .HasMaxLength(ValidationConstants.DefaultTextLength)
            .IsRequired();

        builder.Property(p => p.Start)
            .HasVogenConversion()
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion(x => x.ToString().ToLowerInvariant(), x => Enum.Parse<ProjectStatus>(x, true))
            .IsRequired();

        builder.Property(p => p.End)
            .HasVogenConversion()
            .IsRequired();
    }
}

[EfCoreConverter<ProjectColor>]
[EfCoreConverter<ProjectDescription>]
[EfCoreConverter<ProjectEnd>]
[EfCoreConverter<ProjectId>]
[EfCoreConverter<ProjectName>]
[EfCoreConverter<ProjectStart>]
internal sealed partial class ProjectEfCoreConverters;
