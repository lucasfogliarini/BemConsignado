using BemConsignado.HttpService.Domain.Proponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BemConsignado.HttpService.Infrastructure.EntityConfigurations;

public class ProponentEntityConfiguration : IEntityTypeConfiguration<Proponent>
{
    public void Configure(EntityTypeBuilder<Proponent> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(e => e.Cpf).IsRequired();
        builder.Property(e => e.Email).IsRequired();
        builder.Property(e => e.Address).IsRequired();
        builder.Property(e => e.Income).IsRequired();

        builder
            .HasMany(p => p.Proposals)
            .WithOne(b=>b.Proponent)
            .IsRequired();
    }
}
