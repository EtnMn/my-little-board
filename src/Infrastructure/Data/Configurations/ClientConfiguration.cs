using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etn.MyLittleBoard.Infrastructure.Data.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(p => p.Id)
            .HasValueGenerator<IdValueGenerator<AppDbContext, Client, ClientId>>()
            .HasVogenConversion()
            .IsRequired();

        builder.Property(p => p.Name)
            .HasVogenConversion()
            .HasMaxLength(ValidationConstants.DefaultTextLength)
            .IsRequired();

        builder.Property(p => p.Note)
            .HasVogenConversion()
            .HasMaxLength(ValidationConstants.LongTextLength)
            .IsRequired();

        builder.Property(p => p.State)
            .HasVogenConversion()
            .HasDefaultValue(ClientState.Enabled)
            .IsRequired();
    }
}

[EfCoreConverter<ClientId>]
[EfCoreConverter<ClientName>]
[EfCoreConverter<ClientNote>]
[EfCoreConverter<ClientState>]
internal sealed partial class ClientEfCoreConverters;
